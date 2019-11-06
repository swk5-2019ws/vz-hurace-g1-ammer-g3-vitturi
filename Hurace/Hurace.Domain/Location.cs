using System.Collections.Generic;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("locations")]
    public class Location : DataObject
    {
        [Column("name")]
        public string Name
        {
            get => default;
            set { }
        }

        public Country Country
        {
            get => default;
            set { }
        }

        public IEnumerable<RaceType> RaceTypes
        {
            get => default;
            set { }
        }
    }
}