﻿using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("run")]
    public class Run : DataObject
    {
        [ForeignKey("skier_id")]
        public Skier Skier { get; set; }

        [ForeignKey("race_id")]
        public Race Race { get; set; }

        [Column("run_number")]
        public int RunNumber { get; set; }

        [Column("start_position")]
        public int StartPosition { get; set; }

        [Ignore]
        public int EndPosition { get; set; }

        [Column("status")]
        public RunStatus Status { get; set; }

        [Column("total_time")]
        public double TotalTime { get; set; }
    }
}