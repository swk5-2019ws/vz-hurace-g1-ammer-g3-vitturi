using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurace.Domain;
using Hurace.Timer;

namespace Hurace.Core.Services
{
    public class RunService : Service
    {
        public delegate void SensorMeasurementAddedHandler(Race race, int runNumber, Skier skier, TimeSpan timeSpan);

        public delegate void RunStatusChangedHandler(Race race, int runNumber, Skier skier, RunStatus runStatus);

        public event SensorMeasurementAddedHandler SensorMeasurementAdded;

        public event RunStatusChangedHandler RunStatusChanged;

        public RunService(DaoProvider daoProvider, IRaceClock raceClock) : base(daoProvider)
        {
            raceClock.TimingTriggered += HandleNewSensorMeasurement;
        }

        public async Task UpdateRunStatus(Race race, int runNumber, Skier skier, RunStatus status)
        {
            Run run = await DaoProvider.RunDao.GetBySkierAndRace(race, runNumber, skier).ConfigureAwait(false);
            run.Status = status;
            await DaoProvider.RunDao.Update(run).ConfigureAwait(false);
            RunStatusChanged?.Invoke(race, runNumber, skier, status);
        }

        public async Task<IList<TimeSpan>> GetInterimTimes(Race race, int runNumber, Skier skier)
        {
            Run run = await DaoProvider.RunDao.GetBySkierAndRace(race, runNumber, skier);
            var sensorMeasurements = (await DaoProvider.SensorMeasurementDao.GetMeasurementsForRun(run)).ToArray();

            IList<TimeSpan> interimTimes = new List<TimeSpan>();

            if (sensorMeasurements.Length == 0)
            {
                return interimTimes;
            }

            double lastTimestamp = sensorMeasurements[0].Timestamp;
            for (int i = 1; i < sensorMeasurements.Length; i++)
            {
                var interimTime = sensorMeasurements[i].Timestamp - lastTimestamp;
                interimTimes.Add(TimeSpan.FromSeconds(interimTime));
                lastTimestamp = sensorMeasurements[i].Timestamp;
            }

            return interimTimes;
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
                Timestamp = time.Millisecond / 1000.0
            };

            var sensorMeasurements = (await DaoProvider.SensorMeasurementDao.GetMeasurementsForRun(run)).ToArray();

            if (
                // First measurement
                (sensorId == 0 && sensorMeasurements.Length == 0) ||

                // Sequential measurement
                (sensorId == sensorMeasurements.Last().SensorId + 1)
            )
            {
                await DaoProvider.SensorMeasurementDao.Insert(sensorMeasurement);

                var interimTime = sensorMeasurement.Timestamp - sensorMeasurements.Last().Timestamp;
                var timeSpan = TimeSpan.FromMilliseconds(interimTime * 1000);
                SensorMeasurementAdded?.Invoke(run.Race, run.RunNumber, run.Skier, timeSpan);
            }
        }
    }
}