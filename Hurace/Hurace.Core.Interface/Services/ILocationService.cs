﻿using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Interface.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetLocations();
        Task<Location> GetLocation(int id);
    }
}
