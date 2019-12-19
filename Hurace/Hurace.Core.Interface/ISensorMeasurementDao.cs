using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Domain;

namespace Hurace.Core.Interface
{
    public interface ISensorMeasurementDao : IDataObjectDao<SensorMeasurement>
    {
        Task<IEnumerable<SensorMeasurement>> GetMeasurementForRun(Run run);
    }
}