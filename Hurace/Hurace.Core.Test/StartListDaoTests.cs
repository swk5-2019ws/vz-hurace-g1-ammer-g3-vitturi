using Hurace.Core.Daos;
using Hurace.Core.Interface;
using Hurace.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Hurace.Core.Test
{
    public class StartListDaoTests
    {
        public static async Task<StartList> InsertStartList(ConnectionFactory connectionFactory,
            RaceData raceData = null)
        {
            IStartListDao startListDao = new StartListDao(connectionFactory);

            raceData ??= await RaceDataDaoTests.InsertRaceData(connectionFactory);
            StartList startList = new StartList
            {
                Number = 1,
                RaceData = raceData
            };
            startList.Id = await startListDao.Insert(startList);

            return startList;
        }

        [Fact]
        public async void TestFindAll()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IStartListDao startListDao = new StartListDao(connectionFactory);

            foreach (var _ in await startListDao.FindAll())
                Assert.True(false, "FindAll should return an empty collection");

            await InsertStartList(connectionFactory);
            foreach (var _ in await startListDao.FindAll())
                return;
            Assert.True(false, "FindAll should return a non-empty collection");
        }

        [Fact]
        public async void TestFindById()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IStartListDao startListDao = new StartListDao(connectionFactory);

            StartList startList = await InsertStartList(connectionFactory);

            StartList startListFound = await startListDao.FindById(startList.Id);
            Assert.Equal(startList.Number, startListFound.Number);
        }

        [Fact]
        public async void TestUpdate()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IStartListDao startListDao = new StartListDao(connectionFactory);

            StartList startList = await InsertStartList(connectionFactory);
            startList.Number = 2;
            await startListDao.Update(startList);

            StartList startListAfter = await startListDao.FindById(startList.Id);
            Assert.Equal(startList.Number, startListAfter.Number);
        }

        [Fact]
        public async void TestInsert()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);

            StartList startList = await InsertStartList(connectionFactory);
            Assert.True(startList.Id > 0);
        }

        [Fact]
        public async void TestDelete()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IStartListDao startListDao = new StartListDao(connectionFactory);

            StartList startList = await InsertStartList(connectionFactory);
            Assert.NotNull(await startListDao.FindById(startList.Id));

            await startListDao.Delete(startList.Id);
            Assert.Null(await startListDao.FindById(startList.Id));
        }
    }
}