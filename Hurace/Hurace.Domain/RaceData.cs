using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("race_data")]
    public class RaceData : DataObject
    {
        [Column("race_status")]
        public RaceStatus RaceStatus
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

        [ForeignKey("race_id")]
        public Race Race
        {
            get => default;
            set { }
        }

        [ForeignKey("skier_id")]
        public Skier Skier
        {
            get => default;
            set { }
        }

        [Column("run_number")]
        public int RunNumber
        {
            get => default;
            set { }
        }
    }
}