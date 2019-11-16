using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("race_data")]
    public class RaceData : DataObject
    {
        [Column("race_status")]
        public RaceStatus RaceStatus { get; set; }

        [Column("time")]
        public double Time { get; set; }

        [ForeignKey("skier_id")]
        public Skier Skier { get; set; }
        
        [ForeignKey("race_id")]
        public Race Race { get; set; }

        [Column("run_number")]
        public int RunNumber { get; set; }
    }
}