using System;
using System.Collections;
using System.Collections.Generic;
using Hurace.Domain;

namespace Hurace.Api.Models
{
    public class Season
    {
        public DateTime Year { get; set; }
        public IEnumerable<SkiEvent> Events { get; set; }
    }
}