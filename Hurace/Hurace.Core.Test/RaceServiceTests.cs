using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurace.Core.Interface;
using Hurace.Core.Services;
using Hurace.Domain;
using Moq;
using Xunit;

namespace Hurace.Core.Test
{
    public class RaceServiceTests
    {
        [Fact]
        public void TestRaceStatusChangedEventInvocation()
        {
            // Setup
            var raceDaoMock = new Mock<IRaceDao>();
            raceDaoMock.Setup(d => d.FindById(It.IsAny<int>()))
                .Returns(Task.FromResult(
                    new Race {Status = RaceStatus.Ready})
                );

            var daoProvider = DaoProviderHelper.GetPartialDaoProvider(raceDao: raceDaoMock.Object);
            var raceService = new RaceService(daoProvider);

            var eventTriggered = false;
            raceService.RaceStatusChanged += (race, status) => eventTriggered = true;

            // Execute and Assert
            raceService.EditRace(new Race {Status = RaceStatus.Ready});
            Assert.False(eventTriggered);

            raceService.EditRace(new Race {Status = RaceStatus.InProgress});
            Assert.True(eventTriggered);
        }

        [Fact]
        public void TestAutomaticStartListCreation()
        {
            // Setup
            var skiers = new[] {new Skier(), new Skier(), new Skier()};

            var raceDaoMock = new Mock<IRaceDao>();
            var runDaoMock = new Mock<IRunDao>();

            var daoProvider = DaoProviderHelper.GetPartialDaoProvider(
                raceDao: raceDaoMock.Object,
                runDao: runDaoMock.Object
            );
            var raceService = new RaceService(daoProvider);

            // Execute
            raceService.CreateRace(new Race(), skiers.ToList());

            // Assert
            runDaoMock.Verify(
                d => d.InsertMany(It.IsAny<IEnumerable<Run>>()),
                Times.Once()
            );
        }

        [Fact]
        public void TestAutomaticInvertedStartListCreation()
        {
            // Setup
            var lastRunStatuses = new[] {RunStatus.Completed, RunStatus.Disqualified, RunStatus.Completed};
            IEnumerable<Run> newRunsAdded = null;

            var raceDaoMock = new Mock<IRaceDao>();
            var runDaoMock = new Mock<IRunDao>();
            runDaoMock.Setup(d => d.GetAllRunsForRace(It.IsAny<Race>(), It.IsAny<int>()))
                .Returns(Task.FromResult(
                    lastRunStatuses.Select(s => new Run {Status = s})
                ));
            runDaoMock.Setup(d => d.InsertMany(It.IsAny<IEnumerable<Run>>()))
                .Callback<IEnumerable<Run>>(runs =>
                    newRunsAdded = runs
                );

            var daoProvider = DaoProviderHelper.GetPartialDaoProvider(
                raceDao: raceDaoMock.Object,
                runDao: runDaoMock.Object
            );
            var raceService = new RaceService(daoProvider);

            // Execute
            raceService.CompleteRun(new Race(), 1);

            // Assert
            runDaoMock.Verify(
                d => d.InsertMany(It.IsAny<IEnumerable<Run>>()),
                Times.Once()
            );
            Assert.Equal(2, newRunsAdded.ToArray().Length);
        }
    }
}