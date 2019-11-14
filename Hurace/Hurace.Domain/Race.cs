using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("race")]
    public class Race : DataObject
    {
        [Column("date")]
        public DateTime Date
        {
            get => default;
            set { }
        }

        [Column("name")]
        public string Name
        {
            get => default;
            set { }
        }

        [Column("description")]
        public string Description
        {
            get => default;
            set { }
        }

        [ForeignKey("gender")]
        public Gender Gender
        {
            get => default;
            set { }
        }

        [Column("number_of_sensors")]
        public int NumberOfSensors
        {
            get => default;
            set { }
        }

        [Column("race_type_name")]
        public RaceType RaceType
        {
            get => default;
            set { }
        }

        [Column("website")]
        public string Website
        {
            get => default;
            set { }
        }

        [ForeignKey("location_id")]
        public Location Location
        {
            get => default;
            set
            {
            }
        }
    }
}