using System.Collections.Generic;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("race_data")]
    public class RaceData : DataObject
    {
        public RaceStatus RaceStatus
        {
            get => default;
            set { }
        }

        public ICollection<SensorData> SensorData
        {
            get => default;
            set { }
        }

        [Column("position")]
        public int Position
        {
            get => default;
            set { }
        }

        public Run Run
        {
            get => default;
            set { }
        }
    }
}