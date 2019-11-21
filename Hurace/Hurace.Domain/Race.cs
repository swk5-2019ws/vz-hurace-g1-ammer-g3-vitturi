using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("race")]
    public class Race : DataObject
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [ForeignKey("location_id")]
        public Location Location { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }

        [Column("race_type")]
        public RaceType RaceType { get; set; }
        
        [Column("description")]
        public string Description { get; set; }

        [Column("website")]
        public string Website { get; set; }
        
        [Column("number_of_sensors")]
        public int NumberOfSensors { get; set; }
    }
}