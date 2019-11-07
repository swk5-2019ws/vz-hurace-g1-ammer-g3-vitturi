using Hurace.Core.Interface;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class SensorDataDao : DataObjectDao<SensorData>, ISensorDataDao
    {
        public SensorDataDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}