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
        }
    }
}