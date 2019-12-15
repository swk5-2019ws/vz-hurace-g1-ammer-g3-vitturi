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

        private string _pictureUrl;

        [Column("picture_url")]
        public string PictureUrl
        {
            get => _pictureUrl;
            set
            {
                _pictureUrl = value.Length == 0 ? null : value;
            }
        }

        [Column("archived")]
        public bool Archived { get; set; }

        [ForeignKey("country_id")]
        public Country Country { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }
    }
}