using System;

namespace Hurace.Core.Mapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute
    {
        public string Name { get; set; }

        public ForeignKey(string name)
        {
            this.Name = name;
        }
    }
}