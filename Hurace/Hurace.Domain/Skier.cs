using System;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("skier")]
    public class Skier : DataObject
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("birthdate")]
        public DateTime Birthdate { get; set; }

        [Column("picture_url")]
        public string PictureUrl { get; set; }

        [Column("archived")]
        public bool Archived { get; set; }

        [ForeignKey("country_code")]
        public Country Country { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }
    }
}