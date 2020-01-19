using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurace.Core.Helper;
using Hurace.Core.Interface.Services;
using Hurace.Domain;
using Hurace.Timer;

namespace Hurace.Core.Services
{
    public class RunService : Service, IRunService
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public RunService(DaoProvider daoProvider, IRaceClock raceClock) : base(daoProvider)
        {
            raceClock.TimingTriggered += HandleNewSensorMeasurement;
        }

        public event SensorMeasurementAddedHandler SensorMeasurementAdded;

        public event RunStartedHandler RunStarted;

        public event RunStatusChangedHandler RunStatusChanged;

        public event LeaderBoardUpdateHandler LeaderBoardUpdated;

        public async Task UpdateRunStatus(Race race, int runNumber, Skier skier, RunStatus status)
        {
            var run = await DaoProvider.RunDao.GetBySkierAndRace(race, runNumber, skier);
            run.Status = status;
            await DaoProvider.RunDao.Update(run);
            RunStatusChanged?.Invoke(race, runNumber, skier, status);
        }

        public async Task<IList<TimeSpan>> GetInterimTimes(Race race, int runNumber, Skier skier)
        {
            var run = await DaoProvider.RunDao.GetBySkierAndRace(race, runNumber, skier);
            if (run == null) return new List<TimeSpan>();

            var sensorMeasurements = (await DaoProvider.SensorMeasurementDao.GetMeasurementsForRun(run)).ToArray();

            IList<TimeSpan> interimTimes = new List<TimeSpan>();

            if (sensorMeasurements.Length == 0) return interimTimes;

            var lastTimestamp = sensorMeasurements[0].Timestamp;
            for (var i = 1; i < sensorMeasurements.Length; i++)
            {
                var interimTime = run.RunNumber == 2
                    ? run.TotalTime + sensorMeasurements[i].Timestamp - lastTimestamp
                    : sensorMeasurements[i].Timestamp - lastTimestamp;
                interimTimes.Add(TimeSpan.FromSeconds(interimTime));
            }

            return interimTimes;
        }

        public async Task<IList<TimeSpan>> GetInterimTimesDifferences(IList<TimeSpan> interimTimes,
            Race race, int runNumber, Skier skier)
        {
            var interimTimesDifferences = new List<TimeSpan>();

            var bestRun = (await GetLeaderBoard(race, runNumber)).ToList().FirstOrDefault();
            if (bestRun == null || bestRun.Status != RunStatus.Completed) return interimTimesDifferences;

            var bestRunInterimTimes = await GetInterimTimes(bestRun.Race, bestRun.RunNumber, bestRun.Skier);
            for (var i = 0; i < Math.Min(interimTimes.Count, bestRunInterimTimes.Count); i++)
                interimTimesDifferences.Add(interimTimes[i] - bestRunInterimTimes[i]);

            return interimTimesDifferences;
        }

        public async Task<IEnumerable<Run>> GetAllRunsForRace(Race race, int runNumber)
        {
            return await DaoProvider.RunDao.GetAllRunsForRace(race, runNumber);
        }

        public async Task<Run> GetCurrentRun()
        {
            return await DaoProvider.RunDao.GetCurrentRun();
        }

        public async Task<IEnumerable<Run>> GetLeaderBoard(Race race, int runNumber)
        {
            var runs = (await DaoProvider.RunDao.GetAllRunsForRace(race, runNumber)).ToArray();
            Array.Sort(runs, (x, y) =>
            {
                var timeX = (int) x.TotalTime;
                var timeY = (int) y.TotalTime;

                // Push unfinished runs to the bottom
                if (timeX == 0) timeX = int.MaxValue;
                if (timeY == 0) timeY = int.MaxValue;

                return timeX.CompareTo(timeY);
            });

            for (var i = 0; i < runs.Length; i++) runs[i].EndPosition = i + 1;

            return runs;
        }

        public async Task<int> GetAmountOfRuns()
        {
            return await DaoProvider.RunDao.GetAmountOfRuns().ConfigureAwait(false);
        }

        public async Task<Run> GetRun(int id)
        {
            return await DaoProvider.RunDao.FindById(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Run>> GetRunsForSkierInSeasons(int skierId, uint season)
        {
            var skier = await DaoProvider.SkierDao.FindById(skierId).ConfigureAwait(false);

            if (skier == null) return null;

            var runs = await DaoProvider.RunDao.GetAllRunsForSkier(skier).ConfigureAwait(false);
            var seasonsStart = SeasonParser.GetSeasonsStart(season);
            var seasonsEnd = SeasonParser.GetSeasonsEnd(season);
            return runs.Where(run => run.Race.Date >= seasonsStart && run.Race.Date <= seasonsEnd);
        }

        private void HandleNewSensorMeasurement(int sensorId, DateTime time)
        {
            Task.WaitAll(HandleNewSensorMeasurementAsync(sensorId, time));
        }

        private async Task HandleNewSensorMeasurementAsync(int sensorId, DateTime time)
        {
            var run = await DaoProvider.RunDao.GetCurrentRun();
            if (run == null) return;

            var sensorMeasurement = new SensorMeasurement
            {
                SensorId = sensorId,
                Run = run,
                Timestamp = (time - UnixEpoch).TotalMilliseconds
            };

            var sensorMeasurements = (await DaoProvider.SensorMeasurementDao.GetMeasurementsForRun(run)).ToArray();
            if (sensorId > 0 && sensorMeasurements.Length == 0) return;

            if (
                // First measurement
                sensorId == 0 && sensorMeasurements.Length == 0 ||

                // Sequential measurement
                sensorMeasurements.Length > 0 && sensorId == sensorMeasurements.Last().SensorId + 1
            )
            {
                await DaoProvider.SensorMeasurementDao.Insert(sensorMeasurement);

                if (sensorId > 0)
                {
                    var interimTime = sensorMeasurement.Timestamp - sensorMeasurements.Last().Timestamp;
                    var timeSpan = TimeSpan.FromMilliseconds(interimTime);
                    SensorMeasurementAdded?.Invoke(run.Race, run.RunNumber, run.Skier, timeSpan);
                }
                else if (sensorId == 0)
                {
                    RunStarted?.Invoke(run.Race, run.RunNumber, run.Skier);
                }
            }

            // Last measurement
            if (sensorId == run.Race.NumberOfSensors - 1)
            {
                run.TotalTime = run.RunNumber == 2
                    ? run.TotalTime + sensorMeasurement.Timestamp - sensorMeasurements[0].Timestamp
                    : sensorMeasurement.Timestamp - sensorMeasurements[0].Timestamp;
                run.Status = RunStatus.Completed;
                await DaoProvider.RunDao.Update(run);

                var newLeaderBoard = await GetLeaderBoard(run.Race, run.RunNumber);

                RunStatusChanged?.Invoke(run.Race, run.RunNumber, run.Skier, run.Status);
                LeaderBoardUpdated?.Invoke(run.Race, run.RunNumber, newLeaderBoard);
            }
        }
    }
}