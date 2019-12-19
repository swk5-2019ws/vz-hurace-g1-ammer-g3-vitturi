using System.Threading.Tasks;
using Hurace.Domain;

namespace Hurace.Core.Services
{
    public class RunService : Service
    {
        public RunService(DaoProvider daoProvider) : base(daoProvider)
        {
        }

        public async Task UpdateRunStatus(Race race, int runNumber, Skier skier, RunStatus status)
        {
            Run run = await DaoProvider.RunDao.GetBySkierAndRace(race, runNumber, skier).ConfigureAwait(false);
            run.Status = status;
            await DaoProvider.RunDao.Update(run).ConfigureAwait(false);

        public async Task<IList<TimeSpan>> GetInterimTimes(Race race, int runNumber, Skier skier)
        {
            Run run = await DaoProvider.RunDao.GetBySkierAndRace(race, runNumber, skier).ConfigureAwait(false);
            var sensorMeasurements =
                await DaoProvider.SensorMeasurementDao.GetMeasurementForRun(run).ConfigureAwait(false);

            IList<TimeSpan> interimTimes = new List<TimeSpan>();
            foreach (var sensorMeasurement in sensorMeasurements)
            {
                int seconds = (int) sensorMeasurement.InterimTime;
                int milliseconds = (int) ((sensorMeasurement.InterimTime - seconds) * 1000);

                interimTimes.Add(new TimeSpan(
                    days: 0, hours: 0, minutes: 0,
                    seconds, milliseconds)
                );
            }

            return interimTimes;
        }
    }
}