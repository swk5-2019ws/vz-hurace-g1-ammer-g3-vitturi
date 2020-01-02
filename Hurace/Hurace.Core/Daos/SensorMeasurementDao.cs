using Hurace.Core.Interface.Daos;
using Hurace.Core.Mapper;
using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Daos
{
    public class SensorMeasurementDao : DataObjectDao<SensorMeasurement>, ISensorMeasurementDao
    {
        public SensorMeasurementDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<SensorMeasurement>> GetMeasurementsForRun(Run run)
        {
            using var connection = ConnectionFactory.CreateConnection();
            return await connection.Query<SensorMeasurement>(@"
                SELECT * FROM sensor_measurement
                WHERE run_id = @RunId
                ORDER BY sensor_id",
                new { RunId = run.Id }
            ).ConfigureAwait(false);
        }
    }
}