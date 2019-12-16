using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Domain;

namespace Hurace.Core.Services
{
    public class CountryService : Service
    {
        public CountryService(DaoProvider daoProvider) : base(daoProvider)
        {
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await DaoProvider.CountryDao.FindAll().ConfigureAwait(false);
        }
    }
}