using Hurace.Core.Interface;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class LocationDao : DataObjectDao<Location>, ILocationDao
    {
        public LocationDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}