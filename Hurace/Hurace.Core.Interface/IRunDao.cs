using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Domain;

namespace Hurace.Core.Interface
{
    public interface IRunDao : IDataObjectDao<Run>
    {
        Task<Run> GetBySkierAndRace(Race race, int runNumber, Skier skier);
        Task<IEnumerable<Run>> GetAllRunsForRace(Race race, int runNumber);
        Task DeleteAllRunsForRace(Race race, int runNumber);
        Task<Run> GetCurrentRun();
    }
}