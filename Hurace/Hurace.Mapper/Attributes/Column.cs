using System;

namespace Hurace.Core.Mapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Column : Attribute
    {
        public Column(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}