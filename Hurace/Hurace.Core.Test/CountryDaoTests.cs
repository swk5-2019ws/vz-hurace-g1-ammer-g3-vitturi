using Hurace.Core.Daos;
using Hurace.Core.Interface.Daos;
using Hurace.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Hurace.Core.Test
{
    public class CountryDaoTests
    {
        public static async Task<Country> InsertCountry(ConnectionFactory connectionFactory)
        {
            ICountryDao countryDao = new CountryDao(connectionFactory);

            Country country = new Country
            {
                Code = "AT",
            };
            country.Id = await countryDao.Insert(country);

            return country;
        }

        [Fact]
        public async void TestFindAll()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ICountryDao countryDao = new CountryDao(connectionFactory);

            foreach (var _ in await countryDao.FindAll())
                Assert.True(false, "FindAll should return an empty collection");

            Country country = new Country { Code = "AT" };
            await countryDao.Insert(country);
            foreach (var _ in await countryDao.FindAll())
                return;
            Assert.True(false, "FindAll should return a non-empty collection");
        }

        [Fact]
        public async void TestFindById()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ICountryDao countryDao = new CountryDao(connectionFactory);

            Country country = new Country { Code = "AT" };
            country.Id = await countryDao.Insert(country);

            Country countryFound = await countryDao.FindById(country.Id);
            Assert.Equal(country.Code, countryFound.Code);
        }

        [Fact]
        public async void TestUpdate()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ICountryDao countryDao = new CountryDao(connectionFactory);

            Country country = new Country { Code = "DE" };
            country.Id = await countryDao.Insert(country);

            country.Code = "IT";
            await countryDao.Update(country);
            Country countryAfter = await countryDao.FindById(country.Id);
            Assert.Equal(country.Code, countryAfter.Code);
        }

        [Fact]
        public async void TestInsert()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ICountryDao countryDao = new CountryDao(connectionFactory);

            Country country = new Country { Code = "AT" };
            country.Id = await countryDao.Insert(country);
            Assert.True(country.Id > 0);
        }

        [Fact]
        public async void TestDelete()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ICountryDao countryDao = new CountryDao(connectionFactory);

            Country country = new Country { Code = "AT" };
            country.Id = await countryDao.Insert(country);
            Assert.NotNull(await countryDao.FindById(country.Id));

            await countryDao.Delete(country.Id);
            Assert.Null(await countryDao.FindById(country.Id));
        }
    }
}