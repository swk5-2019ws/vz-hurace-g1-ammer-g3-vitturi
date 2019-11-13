using System;
using System.Collections.Generic;
using System.Text;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("competitions")]
    public class Competition: DataObject
    {
        [ForeignKey("race_type_id")]
        public RaceType RaceType
        {
            get => default;
            set { }
        }

        [ForeignKey("location_id")]
        public Location Location
        {
            get => default;
            set { }
        }
    }
}
