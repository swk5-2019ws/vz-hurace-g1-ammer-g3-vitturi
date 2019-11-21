using System;

namespace Hurace.Core.Mapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
        public KeyAttribute(bool generated = true)
        {
            Generated = generated;
        }

        public bool Generated { get; set; }
    }
}