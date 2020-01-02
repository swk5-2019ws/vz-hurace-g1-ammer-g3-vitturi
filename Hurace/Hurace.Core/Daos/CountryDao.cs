using Hurace.Core.Interface.Daos;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class CountryDao : DataObjectDao<Country>, ICountryDao
    {
        public CountryDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}