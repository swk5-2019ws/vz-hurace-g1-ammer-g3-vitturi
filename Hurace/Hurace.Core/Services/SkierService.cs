using Hurace.Core.Interface.Services;
using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Services
{
    public class SkierService : Service, ISkierService
    {
        public SkierService(DaoProvider daoProvider) : base(daoProvider)
        {
        }

        public async Task ArchiveSkier(Skier skier)
        {
            skier.Archived = true;
            await DaoProvider.SkierDao.Update(skier).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Skier>> GetSkiers(Gender gender, string? nameSubstring = null)
        {
            return await DaoProvider.SkierDao.FindByFilters(gender: gender, nameSubstring: nameSubstring).ConfigureAwait(false);
        }
        
        public async Task<int> GetAmountOfSkiers()
        {
            return await DaoProvider.SkierDao.GetAmountOfSkiers().ConfigureAwait(false);
        }
    }
}