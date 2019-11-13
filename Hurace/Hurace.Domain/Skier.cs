using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("skiers")]
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

        [Column("profile_image")]
        public byte[] ProfileImage
        {
            get => default;
            set { }
        }

        [Column("archived")]
        public int Archived
        {
            get => default;
            set { }
        }
        
        [ForeignKey("national_code")]
        public Country Country
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
    }
}