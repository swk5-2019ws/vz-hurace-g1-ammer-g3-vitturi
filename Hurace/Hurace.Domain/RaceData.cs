﻿using System.Collections.Generic;

namespace Hurace.Domain
{
    public class RaceData : DataObject
    {
        public RaceStatus RaceStatus
        {
            get => default;
            set { }
        }

        public IEnumerable<SensorData> SensorData
        {
            get => default;
            set { }
        }
    }
}