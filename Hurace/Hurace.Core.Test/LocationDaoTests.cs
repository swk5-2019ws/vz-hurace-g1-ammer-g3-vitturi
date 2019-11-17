using Hurace.Core.Daos;
using Hurace.Core.Interface;
using Hurace.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Hurace.Core.Test
{
    public class LocationDaoTests
    {
        public static async Task<Location> InsertLocation(ConnectionFactory connectionFactory, Country country = null)
        {
            ILocationDao locationDao = new LocationDao(connectionFactory);

            country ??= await CountryDaoTests.InsertCountry(connectionFactory);
            Location location = new Location
            {
                Country = country,
                Name = "Kitzbühel"
            };
            location.Id = await locationDao.Insert(location);

            return location;
        }

        [Fact]
        public async void TestFindAll()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ILocationDao locationDao = new LocationDao(connectionFactory);

            foreach (var _ in await locationDao.FindAll())
                Assert.True(false, "FindAll should return an empty collection");

            await InsertLocation(connectionFactory);
            foreach (var _ in await locationDao.FindAll())
                return;
            Assert.True(false, "FindAll should return a non-empty collection");
        }

        [Fact]
        public async void TestFindById()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ILocationDao locationDao = new LocationDao(connectionFactory);

            Location location = await InsertLocation(connectionFactory);

            Location locationFound = await locationDao.FindById(location.Id);
            Assert.Equal(location.Name, locationFound.Name);
        }

        [Fact]
        public async void TestUpdate()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ILocationDao locationDao = new LocationDao(connectionFactory);

            Location location = await InsertLocation(connectionFactory);
            location.Name = "Hinterstoder";
            await locationDao.Update(location);

            Location locationAfter = await locationDao.FindById(location.Id);
            Assert.Equal(location.Name, locationAfter.Name);
        }

        [Fact]
        public async void TestInsert()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);

            Location location = await InsertLocation(connectionFactory);
            Assert.True(location.Id > 0);
        }

        [Fact]
        public async void TestDelete()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ILocationDao locationDao = new LocationDao(connectionFactory);

            Location location = await InsertLocation(connectionFactory);
            Assert.NotNull(await locationDao.FindById(location.Id));

            await locationDao.Delete(location.Id);
            Assert.Null(await locationDao.FindById(location.Id));
        }
    }
}