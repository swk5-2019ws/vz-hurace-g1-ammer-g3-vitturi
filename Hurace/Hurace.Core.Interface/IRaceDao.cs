using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Domain;

namespace Hurace.Core.Interface
{
    public interface IRaceDao : IDataObjectDao<Race>
    {
        Task<IEnumerable<Race>> FindByName(string nameSubstring);
        Task<IEnumerable<Race>> GetLastRaces(int count);
    }
}