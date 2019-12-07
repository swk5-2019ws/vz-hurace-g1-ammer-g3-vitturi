using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Domain;

#nullable enable
namespace Hurace.Core.Interface
{
    public interface ISkierDao : IDataObjectDao<Skier>
    {
        Task<IEnumerable<Skier>> SearchSkiers(string? nameSubstring = null, Country? country = null,
            Gender? gender = null);
    }
}