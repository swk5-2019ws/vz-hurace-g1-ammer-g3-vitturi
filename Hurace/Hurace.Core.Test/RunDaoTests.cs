using Hurace.Core.Daos;
using Hurace.Core.Interface.Daos;
using Hurace.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Hurace.Core.Test
{
    public class RunDaoTests
    {
        public static async Task<Run> InsertRun(ConnectionFactory connectionFactory,
            Skier skier = null, Race race = null)
        {
            IRunDao runDao = new RunDao(connectionFactory);

            Location location = await LocationDaoTests.InsertLocation(connectionFactory);
            skier ??= await SkierDaoTests.InsertSkier(connectionFactory, location.Country);
            race ??= await RaceDaoTests.InsertRace(connectionFactory, location);
            Run run = new Run
            {
                Skier = skier,
                Race = race,
                RunNumber = 1,
                StartPosition = 1,
                Status = RunStatus.Completed,
                TotalTime = 82.1,
            };
            run.Id = await runDao.Insert(run);

            return run;
        }

        [Fact]
        public async void TestFindAll()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRunDao runDao = new RunDao(connectionFactory);

            foreach (var _ in await runDao.FindAll())
                Assert.True(false, "FindAll should return an empty collection");

            await InsertRun(connectionFactory);
            foreach (var _ in await runDao.FindAll())
                return;
            Assert.True(false, "FindAll should return a non-empty collection");
        }

        [Fact]
        public async void TestFindById()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRunDao runDao = new RunDao(connectionFactory);

            Run run = await InsertRun(connectionFactory);

            Run runFound = await runDao.FindById(run.Id);
            Assert.Equal(run.TotalTime, runFound.TotalTime);
            Assert.Equal(run.Status, runFound.Status);
        }

        [Fact]
        public async void TestUpdate()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRunDao runDao = new RunDao(connectionFactory);

            Run run = await InsertRun(connectionFactory);
            run.TotalTime = 85.11;
            run.Status = RunStatus.InProgress;
            await runDao.Update(run);

            Run runAfter = await runDao.FindById(run.Id);
            Assert.Equal(run.TotalTime, runAfter.TotalTime);
            Assert.Equal(run.Status, runAfter.Status);
        }

        [Fact]
        public async void TestInsert()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);

            Run run = await InsertRun(connectionFactory);
            Assert.True(run.Id > 0);
        }

        [Fact]
        public async void TestDelete()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            IRunDao runDao = new RunDao(connectionFactory);

            Run run = await InsertRun(connectionFactory);
            Assert.NotNull(await runDao.FindById(run.Id));

            await runDao.Delete(run.Id);
            Assert.Null(await runDao.FindById(run.Id));
        }
    }
}