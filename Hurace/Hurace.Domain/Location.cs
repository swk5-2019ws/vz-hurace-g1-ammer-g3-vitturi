using System.Collections.Generic;

namespace Hurace.Domain
{
    public class Location
    {
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