using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("sensor_measurement")]
    public class SensorMeasurement : DataObject
    {
        [Column("sensor_id")]
        public int SensorId
        {
            get => default;
            set { }
        }

        [Column("interim_time")]
        public DateTime InterimTime
        {
            get => default;
            set { }
        }

        [ForeignKey("race_data_id")]
        public RaceData RaceData
        {
            get => default;
            set { }
        }
    }
}