using System;
using System.Collections;
using System.Collections.Generic;
using Hurace.Domain;

namespace Hurace.Api.Models
{
    public class SkiEvent
    {
        public Location Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<RaceType> RaceTypes { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
    }
}