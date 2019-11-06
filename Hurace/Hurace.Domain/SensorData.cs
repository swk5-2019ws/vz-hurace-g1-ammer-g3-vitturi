using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("sensor_data")]
    public class SensorData : DataObject
    {
        [Column("sensor_id")]
        public int SensorId
        {
            get => default;
            set { }
        }

        [Column("time")]
        public DateTime Time
        {
            get => default;
            set { }
        }
    }
}