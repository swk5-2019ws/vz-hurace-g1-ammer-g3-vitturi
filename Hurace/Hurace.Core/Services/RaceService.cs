using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurace.Core.Interface.Services;
using Hurace.Domain;

namespace Hurace.Core.Services
{
    public class RaceService : Service, IRaceService
    {
        private const int AmountSkierSecondRun = 30;

        public RaceService(DaoProvider daoProvider) : base(daoProvider)
        {
        }

        public event RaceStatusChangedHandler RaceStatusChanged;

        public async Task CreateRace(Race race, IList<Skier> skiers)
        {
            race.Id = await DaoProvider.RaceDao.Insert(race).ConfigureAwait(false);
            await CreateStartList(race, 1, skiers).ConfigureAwait(false);
        }

        public async Task EditRace(Race race)
        {
            var savedRace = await DaoProvider.RaceDao.FindById(race.Id).ConfigureAwait(false);
            await DaoProvider.RaceDao.Update(race).ConfigureAwait(false);
            if (race.Status != savedRace.Status) RaceStatusChanged?.Invoke(race, race.Status);
        }

        public async Task CompleteRun(Race race, int runNumber)
        {
            race.CompletedRuns = runNumber;
            await DaoProvider.RaceDao.Update(race).ConfigureAwait(false);

            if (runNumber >= 1)
            {
                // Create inverted start list
                var pastRuns =
                    (await DaoProvider.RunDao.GetAllRunsForRace(race, runNumber).ConfigureAwait(false)).Take(
                        AmountSkierSecondRun);
                var newStartList = new List<Skier>();

                foreach (var pastRun in pastRuns)
                    if (pastRun.Status == RunStatus.Completed)
                        newStartList.Insert(0, pastRun.Skier);

                await CreateSecondStartList(race, pastRuns.Reverse().ToList(), newStartList).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Race>> SearchRaces(string nameSubstring)
        {
            return await DaoProvider.RaceDao.FindByName(nameSubstring).ConfigureAwait(false);
        }

        public async Task<Race> GetCurrentRace()
        {
            return await DaoProvider.RaceDao.GetCurrentRace().ConfigureAwait(false);
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

        public async Task<int> GetAmountOfRaces()
        {
            return await DaoProvider.RaceDao.GetAmountOfRaces().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Race>> GetRaces()
        {
            return await DaoProvider.RaceDao.FindAll().ConfigureAwait(false);
        }

        public async Task<Race> GetRace(int id)
        {
            return await DaoProvider.RaceDao.FindById(id).ConfigureAwait(false);
        }

        private async Task CreateSecondStartList(Race race, IList<Run> firstRuns, IList<Skier> skiers)
        {
            IList<Run> runs = new List<Run>();
            for (var i = 0; i < skiers.Count; i++)
                runs.Add(new Run
                {
                    Race = race,
                    Status = RunStatus.Ready,
                    RunNumber = 2,
                    Skier = skiers[i],
                    TotalTime = firstRuns[i].TotalTime,
                    StartPosition = i + 1
                });

            await DaoProvider.RunDao.InsertMany(runs).ConfigureAwait(false);
        }

        private async Task CreateStartList(Race race, int runNumber, IList<Skier> skiers)
        {
            IList<Run> runs = new List<Run>();
            for (var i = 0; i < skiers.Count; i++)
                runs.Add(new Run
                {
                    Race = race,
                    Status = RunStatus.Ready,
                    RunNumber = runNumber,
                    Skier = skiers[i],
                    StartPosition = i + 1
                });

            await DaoProvider.RunDao.InsertMany(runs).ConfigureAwait(false);
        }
    }
}