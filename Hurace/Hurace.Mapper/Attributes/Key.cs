using System;

namespace Hurace.Core.Mapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Key : Attribute
    {
        public Key(bool generated = true)
        {
            this.Generated = generated;
        }

        public bool Generated { get; set; }
    }
}