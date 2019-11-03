using System;

namespace Hurace.Domain
{
    public abstract class DataObject
    {
        public int Id
        {
            get => default;
            set { }
        }

        public DateTime LastModified
        {
            get => default;
            set { }
        }
    }
}