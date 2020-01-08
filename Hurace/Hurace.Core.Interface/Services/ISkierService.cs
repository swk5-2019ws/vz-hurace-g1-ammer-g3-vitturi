using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Interface.Services
{
    public interface ISkierService
    {
        Task ArchiveSkier(Skier skier);

        Task<IEnumerable<Skier>> GetSkiers(Gender? gender, string? nameSubstring = null);
        
        Task<int> GetAmountOfSkiers();
        
        Task<Skier> GetSkier(int id);
        
        Task<Skier> CreateSkier(Skier skier);
        
        Task<bool> RemoveSkier(int id);
        
        Task<bool> UpdateSkier(Skier skier);
    }
}
