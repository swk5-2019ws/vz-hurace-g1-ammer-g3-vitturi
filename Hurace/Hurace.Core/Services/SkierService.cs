using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurace.Core.Interface.Services;
using Hurace.Domain;

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

        public async Task<IEnumerable<Skier>> GetSkiers(Gender? gender, string? nameSubstring = null)
        {
            var skiers = await DaoProvider.SkierDao.FindByFilters(gender: gender, nameSubstring: nameSubstring)
                .ConfigureAwait(false);
            return skiers.Where(skier => !skier.Archived);
        }

        public async Task<int> GetAmountOfSkiers()
        {
            return await DaoProvider.SkierDao.GetAmountOfSkiers().ConfigureAwait(false);
        }

        public async Task<Skier> GetSkier(int id)
        {
            return await DaoProvider.SkierDao.FindById(id).ConfigureAwait(false);
        }

        public async Task<Skier> CreateSkier(Skier skier)
        {
            var id = await DaoProvider.SkierDao.Insert(skier).ConfigureAwait(false);
            return await DaoProvider.SkierDao.FindById(id).ConfigureAwait(false);
        }

        public async Task<bool> RemoveSkier(int id)
        {
            var skier = await DaoProvider.SkierDao.FindById(id).ConfigureAwait(false);

            if (skier == null) return false;

            var deleted = await DaoProvider.SkierDao.Delete(id);

            if (!deleted)
            {
                skier.Archived = true;
                return await DaoProvider.SkierDao.Update(skier);
            }

            return true;
        }

        public async Task<bool> UpdateSkier(Skier skier)
        {
            return await DaoProvider.SkierDao.Update(skier);
        }
    }
}