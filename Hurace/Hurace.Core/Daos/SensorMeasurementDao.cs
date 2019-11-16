using Hurace.Core.Interface;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class SensorMeasurementDao : DataObjectDao<SensorMeasurement>, ISensorMeasurementDao
    {
        public SensorMeasurementDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}