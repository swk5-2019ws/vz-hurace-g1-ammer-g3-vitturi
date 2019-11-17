using System;
using Hurace.Core.Daos;
using Hurace.Core.Interface;
using Hurace.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Hurace.Core.Test
{
    public class RaceDataDaoTests
    {
        public static async Task<RaceData> InsertRaceData(ConnectionFactory connectionFactory,
            Skier skier = null, Race race = null)
        {
            IRaceDataDao raceDataDao = new RaceDataDao(connectionFactory);

            Location location = await LocationDaoTests.InsertLocation(connectionFactory);
            skier ??= await SkierDaoTests.InsertSkier(connectionFactory, location.Country);
            race ??= await RaceDaoTests.InsertRace(connectionFactory, location);
            RaceData raceData = new RaceData
            {
                RaceStatus = RaceStatus.Completed,
                Time = 82.1,
                Skier = skier,
                Race = race,
                RunNumber = 1
            };
            raceData.Id = await raceDataDao.Insert(raceData);

            return raceData;
        }

        [Fact]
        public async void TestFindAll()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRaceDataDao raceDataDao = new RaceDataDao(connectionFactory);

            foreach (var _ in await raceDataDao.FindAll())
                Assert.True(false, "FindAll should return an empty collection");

            await InsertRaceData(connectionFactory);
            foreach (var _ in await raceDataDao.FindAll())
                return;
            Assert.True(false, "FindAll should return a non-empty collection");
        }

        [Fact]
        public async void TestFindById()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRaceDataDao raceDataDao = new RaceDataDao(connectionFactory);

            RaceData raceData = await InsertRaceData(connectionFactory);

            RaceData raceDataFound = await raceDataDao.FindById(raceData.Id);
            Assert.Equal(raceData.Time, raceDataFound.Time);
        }

        [Fact]
        public async void TestUpdate()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRaceDataDao raceDataDao = new RaceDataDao(connectionFactory);

            RaceData raceData = await InsertRaceData(connectionFactory);
            raceData.Time = 85.11;
            await raceDataDao.Update(raceData);

            RaceData raceDataAfter = await raceDataDao.FindById(raceData.Id);
            Assert.Equal(raceData.Time, raceDataAfter.Time);
        }

        [Fact]
        public async void TestInsert()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);

            RaceData raceData = await InsertRaceData(connectionFactory);
            Assert.True(raceData.Id > 0);
        }

        [Fact]
        public async void TestDelete()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRaceDataDao raceDataDao = new RaceDataDao(connectionFactory);

            RaceData raceData = await InsertRaceData(connectionFactory);
            Assert.NotNull(await raceDataDao.FindById(raceData.Id));

            await raceDataDao.Delete(raceData.Id);
            Assert.Null(await raceDataDao.FindById(raceData.Id));
        }
    }
}