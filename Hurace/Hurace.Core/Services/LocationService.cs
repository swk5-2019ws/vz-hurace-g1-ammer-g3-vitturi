using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Domain;

namespace Hurace.Core.Services
{
    public class LocationService : Service
    {
        public LocationService(DaoProvider daoProvider) : base(daoProvider)
        {
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await DaoProvider.LocationDao.FindAll().ConfigureAwait(false);
        }
    }
}