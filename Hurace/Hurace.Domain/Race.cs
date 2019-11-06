using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("races")]
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

        public RaceType RaceType
        {
            get => default;
            set { }
        }

        public Gender Gender
        {
            get => default;
            set { }
        }

        public Location Location
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
    }
}