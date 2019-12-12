using System;
using Hurace.Core.Daos;
using Hurace.Core.Interface;
using Hurace.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Hurace.Core.Test
{
    public class RaceDaoTests
    {
        public static async Task<Race> InsertRace(ConnectionFactory connectionFactory, Location location = null)
        {
            IRaceDao raceDao = new RaceDao(connectionFactory);

            location ??= await LocationDaoTests.InsertLocation(connectionFactory);
            Race race = new Race
            {
                Name = "Kitzbühel Slalom",
                Date = DateTime.Today,
                Location = location,
                Gender = Gender.Female,
                RaceType = RaceType.Slalom,
                Description = null,
                Website = null,
                NumberOfSensors = 5,
                Status = RaceStatus.Ready
            };
            race.Id = await raceDao.Insert(race);

            return race;
        }

        [Fact]
        public async void TestFindAll()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRaceDao raceDao = new RaceDao(connectionFactory);

            foreach (var _ in await raceDao.FindAll())
                Assert.True(false, "FindAll should return an empty collection");

            await InsertRace(connectionFactory);
            foreach (var _ in await raceDao.FindAll())
                return;
            Assert.True(false, "FindAll should return a non-empty collection");
        }

        [Fact]
        public async void TestFindById()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRaceDao raceDao = new RaceDao(connectionFactory);

            Race race = await InsertRace(connectionFactory);

            Race raceFound = await raceDao.FindById(race.Id);
            Assert.Equal(race.Name, raceFound.Name);
            Assert.Equal(race.Gender, raceFound.Gender);
            Assert.Equal(race.RaceType, raceFound.RaceType);
        }

        [Fact]
        public async void TestUpdate()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRaceDao raceDao = new RaceDao(connectionFactory);

            Race race = await InsertRace(connectionFactory);
            race.Name = "Hinterstoder Slalom";
            race.RaceType = RaceType.SuperSlalom;
            await raceDao.Update(race);

            Race raceAfter = await raceDao.FindById(race.Id);
            Assert.Equal(race.Name, raceAfter.Name);
            Assert.Equal(race.RaceType, raceAfter.RaceType);
        }

        [Fact]
        public async void TestInsert()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);

            Race race = await InsertRace(connectionFactory);
            Assert.True(race.Id > 0);
        }

        [Fact]
        public async void TestDelete()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRaceDao raceDao = new RaceDao(connectionFactory);

            Race race = await InsertRace(connectionFactory);
            Assert.NotNull(await raceDao.FindById(race.Id));

            await raceDao.Delete(race.Id);
            Assert.Null(await raceDao.FindById(race.Id));
        }
    }
}