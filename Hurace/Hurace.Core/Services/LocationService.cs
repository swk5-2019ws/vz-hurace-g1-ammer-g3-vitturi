using Hurace.Core.Interface.Services;
using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Services
{
    public class LocationService : Service, ILocationService
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