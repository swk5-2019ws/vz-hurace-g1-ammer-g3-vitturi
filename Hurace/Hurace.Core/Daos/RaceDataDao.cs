using Hurace.Core.Interface;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class RaceDataDao : DataObjectDao<RaceData>, IRaceDataDao
    {
        public RaceDataDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}