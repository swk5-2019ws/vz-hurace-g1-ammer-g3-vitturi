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

        [ForeignKey("gender_id")]
        public Gender Gender
        {
            get => default;
            set { }
        }

        [ForeignKey("competition_id")]
        public Competition Competition
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