using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Interface.Daos
{
    public interface IRaceDao : IDataObjectDao<Race>
    {
        Task<IEnumerable<Race>> FindByName(string nameSubstring);
        Task<IEnumerable<Race>> GetLastRaces(int count);
        Task<Race> GetCurrentRace();
    }
}