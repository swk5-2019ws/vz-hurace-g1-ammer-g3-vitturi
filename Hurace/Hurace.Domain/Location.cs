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

        [ForeignKey("national_code")]
        public Country Country
        {
            get => default;
            set { }
        }

        public ICollection<RaceType> RaceTypes
        {
            get => default;
            set { }
        }
    }
}