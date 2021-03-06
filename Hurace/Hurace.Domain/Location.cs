﻿using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("location")]
    public class Location : DataObject
    {
        [Column("name")]
        public string Name { get; set; }

        [ForeignKey("country_id")]
        public Country Country { get; set; }
    }
}