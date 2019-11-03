using System;

namespace Hurace.Domain
{
    public class Race : DataObject
    {
        public DateTime StartDate
        {
            get => default;
            set { }
        }

        public DateTime EndDate
        {
            get => default;
            set { }
        }

        public string Name
        {
            get => default;
            set { }
        }

        public string Description
        {
            get => default;
            set { }
        }

        public RaceType RaceType
        {
            get => default;
            set { }
        }

        public Gender Gender
        {
            get => default;
            set { }
        }

        public Location Location
        {
            get => default;
            set { }
        }

        public int NumberOfSensors
        {
            get => default;
            set { }
        }
    }
}