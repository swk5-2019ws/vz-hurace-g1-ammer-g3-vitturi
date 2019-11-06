using System;

namespace Hurace.Core.Mapper.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Table : Attribute
    {
        public Table(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}