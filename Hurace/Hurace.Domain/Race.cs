using Hurace.Core.Mapper.Attributes;
using System;

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

        [Column("number_of_sensors")]
        public int NumberOfSensors { get; set; }

        [Column("completed_runs")]
        public int CompletedRuns { get; set; }

        [Column("status")]
        public RaceStatus Status { get; set; }
    }
}