using Hurace.Core.Interface.Services;
using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Services
{
    public class CountryService : Service, ICountryService
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