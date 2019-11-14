using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("skier")]
    public class Skier : DataObject
    {
        [Column("first_name")]
        public string FirstName
        {
            get => default;
            set { }
        }

        [Column("last_name")]
        public string LastName
        {
            get => default;
            set { }
        }

        [Column("birthdate")]
        public DateTime Birthdate
        {
            get => default;
            set { }
        }

        [Column("picture_url")]
        public string PictureUrl
        {
            get => default;
            set { }
        }

        [Column("archived")]
        public bool Archived
        {
            get => default;
            set { }
        }

        [ForeignKey("country_code")]
        public Country Country
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
    }
}