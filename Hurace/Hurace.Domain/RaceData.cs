using System.Collections.Generic;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("race_data")]
    public class RaceData : DataObject
    {
        [ForeignKey("race_status_id")]
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

        [ForeignKey("run_id")]
        public Run Run
        {
            get => default;
            set { }
        }
    }
}