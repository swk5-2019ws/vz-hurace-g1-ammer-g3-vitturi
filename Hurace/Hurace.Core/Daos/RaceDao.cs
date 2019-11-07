using Hurace.Core.Interface;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class RaceDao : DataObjectDao<Race>, IRaceDao
    {
        public RaceDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}