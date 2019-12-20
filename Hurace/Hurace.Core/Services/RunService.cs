using System;
using System.Collections.Generic;
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
            var sensorMeasurements = (await DaoProvider.SensorMeasurementDao.GetMeasurementForRun(run)).ToArray();

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
    }
}