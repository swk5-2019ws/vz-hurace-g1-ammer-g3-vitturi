using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Domain;

namespace Hurace.Core.Services
{
    public class SkierService : Service
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
    }
}