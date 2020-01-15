using System.Collections;
using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Interface.Services
{
    public delegate void RaceStatusChangedHandler(Race race, RaceStatus raceStatus);
    public interface IRaceService
    {
        event RaceStatusChangedHandler RaceStatusChanged;

        Task CreateRace(Race race, IList<Skier> skiers);

        Task EditRace(Race race);

        Task CompleteRun(Race race, int runNumber);

        Task<IEnumerable<Race>> SearchRaces(string nameSubstring);

        Task<Race> GetCurrentRace();

        Task EditStartList(Race race, int runNumber, IList<Skier> skiers);

        Task<IEnumerable<Race>> GetLastRaces(int shownRaces);
        
        Task<int> GetAmountOfRaces();
        
        Task<IEnumerable<Race>> GetRaces();
        
        Task<Race> GetRace(int id);
    }
}
