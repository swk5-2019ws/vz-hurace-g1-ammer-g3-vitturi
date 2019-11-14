using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("sensor_measurement")]
    public class SensorMeasurement : DataObject
    {
        [Column("sensor_id")]
        public int SensorId { get; set; }

        [Column("interim_time")]
        public DateTime InterimTime { get; set; }

        [ForeignKey("race_data_id")]
        public RaceData RaceData { get; set; }
    }
}