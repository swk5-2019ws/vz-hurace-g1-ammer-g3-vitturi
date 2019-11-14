﻿using System;

namespace Hurace.Core.Mapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute
    {
        public ForeignKey(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}