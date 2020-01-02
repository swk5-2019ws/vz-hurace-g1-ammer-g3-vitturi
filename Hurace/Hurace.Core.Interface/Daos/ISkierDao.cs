using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable
namespace Hurace.Core.Interface.Daos
{
    public interface ISkierDao : IDataObjectDao<Skier>
    {
        Task<IEnumerable<Skier>> FindByFilters(string? nameSubstring = null, Country? country = null,
            Gender? gender = null);
    }
}