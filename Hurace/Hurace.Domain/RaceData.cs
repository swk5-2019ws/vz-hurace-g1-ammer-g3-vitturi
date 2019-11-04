using System.Collections.Generic;

namespace Hurace.Domain
{
    public class RaceData : DataObject
    {
        public RaceStatus RaceStatus
        {
            get => default;
            set { }
        }

        public ICollection<SensorData> SensorData
        {
            get => default;
            set { }
        }

        public int Position
        {
            get => default;
            set { }
        }

        public Run Run
        {
            get => default;
            set { }
        }
    }
}