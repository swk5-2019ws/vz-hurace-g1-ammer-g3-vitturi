using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurace.Domain;

namespace Hurace.Core.Services
{
    public class RaceService : Service
    {
        public delegate void RaceStatusChangedHandler(RaceStatus raceStatus);

        public event RaceStatusChangedHandler RaceStatusChanged;

        public RaceService(DaoProvider daoProvider) : base(daoProvider)
        {
        }

        public async Task CreateRace(Race race, IList<Skier> skiers)
        {
            await DaoProvider.RaceDao.Insert(race).ConfigureAwait(false);
            await CreateStartList(race, 1, skiers).ConfigureAwait(false);
        }

        public async Task EditRace(Race race)
        {
            await DaoProvider.RaceDao.Update(race).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Race>> SearchRaces(string nameSubstring)
        {
            return await DaoProvider.RaceDao.FindByName(nameSubstring).ConfigureAwait(false);
        }

        public async Task CreateStartList(Race race, int runNumber, IList<Skier> skiers)
        {
            IList<Run> runs = new List<Run>();
            for (var i = 0; i < skiers.Count; i++)
            {
                runs.Add(new Run
                {
                    Race = race,
                    Status = RunStatus.Ready,
                    RunNumber = runNumber,
                    Skier = skiers[i],
                    StartPosition = i + 1,
                });
            }

            await DaoProvider.RunDao.InsertMany(runs).ConfigureAwait(false);
        }

        public async Task EditStartList(Race race, int runNumber, IList<Skier> skiers)
        {
            await DaoProvider.RunDao.DeleteAllRunsForRace(race, runNumber).ConfigureAwait(false);
            await CreateStartList(race, runNumber, skiers).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Race>> GetLastRaces(int shownRaces)
        {
            return await DaoProvider.RaceDao.GetLastRaces(shownRaces).ConfigureAwait(false);
        }
    }
}