using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Interface.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountries();
    }
}
