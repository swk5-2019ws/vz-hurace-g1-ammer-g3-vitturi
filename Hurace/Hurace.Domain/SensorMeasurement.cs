using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("sensor_measurement")]
    public class SensorMeasurement : DataObject
    {
        [Column("sensor_id")]
        public int SensorId { get; set; }

        [Column("timestamp")]
        public double Timestamp { get; set; }

        [ForeignKey("run_id")]
        public Run Run { get; set; }
    }
}