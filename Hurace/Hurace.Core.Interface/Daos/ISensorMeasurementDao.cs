using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Interface.Daos
{
    public interface ISensorMeasurementDao : IDataObjectDao<SensorMeasurement>
    {
        Task<IEnumerable<SensorMeasurement>> GetMeasurementsForRun(Run run);
    }
}