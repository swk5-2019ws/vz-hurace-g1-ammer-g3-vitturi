using Hurace.Core.Interface.Daos;
using Hurace.Core.Services;
using Hurace.Domain;
using Hurace.Simulator;
using Hurace.Timer;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hurace.Core.Test
{
    public class RunServiceTests
    {
        [Fact]
        public void TestRunStatusChangedEventInvocation()
        {
            // Setup
            var runDaoMock = new Mock<IRunDao>();
            runDaoMock.Setup(d => d.GetBySkierAndRace(It.IsAny<Race>(), It.IsAny<int>(), It.IsAny<Skier>()))
                .Returns(Task.FromResult(
                    new Run { Status = RunStatus.Ready })
                );

            var daoProvider = DaoProviderHelper.GetPartialDaoProvider(runDao: runDaoMock.Object);
            var runService = new RunService(daoProvider, new SimulatorRaceClock());

            var eventTriggered = false;
            runService.RunStatusChanged += (race, runNumber, skier, runStatus) => eventTriggered = true;

            // Execute
            runService.UpdateRunStatus(null, 1, null, RunStatus.InProgress);

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public async void TestInterimTimesCalculation()
        {
            // Setup
            var timestamps = new[] { 1576850000.000, 1576850001.111, 1576850002.222 };

            var sensorMeasurementDaoMock = new Mock<ISensorMeasurementDao>();
            sensorMeasurementDaoMock.Setup(d => d.GetMeasurementsForRun(It.IsAny<Run>()))
                .Returns(Task.FromResult(
                    timestamps.Select(t => new SensorMeasurement { Timestamp = t })
                ));

            var runDaoMock = new Mock<IRunDao>();
            runDaoMock.Setup(d => d.GetBySkierAndRace(It.IsAny<Race>(), It.IsAny<int>(), It.IsAny<Skier>()))
                .Returns(Task.FromResult(new Run()));

            var daoProvider = DaoProviderHelper.GetPartialDaoProvider(
                sensorMeasurementDao: sensorMeasurementDaoMock.Object,
                runDao: runDaoMock.Object
            );
            var runService = new RunService(daoProvider, new SimulatorRaceClock());

            // Execute
            var calculatedTimespans = (await runService.GetInterimTimes(null, 1, null)).ToArray();

            // Assert
            Assert.Equal(timestamps.Length - 1, calculatedTimespans.Length);
            Assert.Equal(calculatedTimespans[0], TimeSpan.FromSeconds(timestamps[1] - timestamps[0]));
            Assert.Equal(calculatedTimespans[1], TimeSpan.FromSeconds(timestamps[2] - timestamps[0]));
        }

        [Fact]
        public async void TestLeaderBoardCalculation()
        {
            // Setup
            var totalTimes = new[] { 2.2, 1.1, 3.3, 0.0, 0.0 };

            var runDaoMock = new Mock<IRunDao>();
            runDaoMock.Setup(d => d.GetAllRunsForRace(It.IsAny<Race>(), It.IsAny<int>()))
                .Returns(Task.FromResult(
                    totalTimes.Select(t => new Run { TotalTime = t })
                ));

            var daoProvider = DaoProviderHelper.GetPartialDaoProvider(runDao: runDaoMock.Object);
            var runService = new RunService(daoProvider, new SimulatorRaceClock());

            // Execute
            var leaderBoard = (await runService.GetLeaderBoard(null, 1)).ToArray();

            // Assert
            Assert.Equal(totalTimes.Length, leaderBoard.Length);
            Assert.Equal(1.1, leaderBoard[0].TotalTime);
            Assert.Equal(2.2, leaderBoard[1].TotalTime);
            Assert.Equal(3.3, leaderBoard[2].TotalTime);
            Assert.Equal(0.0, leaderBoard[3].TotalTime);
            Assert.Equal(0.0, leaderBoard[4].TotalTime);
        }

        private static RunService GetRunServiceForSensorImpulseHandlingTest(IRaceClock raceClock)
        {
            var existingMeasurementsTimestamps = new[] { 1576850000.000, 1576850001.111, 1576850002.222 };

            var sensorMeasurementDaoMock = new Mock<ISensorMeasurementDao>();
            sensorMeasurementDaoMock.Setup(d => d.GetMeasurementsForRun(It.IsAny<Run>()))
                .Returns(Task.FromResult(
                    existingMeasurementsTimestamps.Select((t, i) => new SensorMeasurement
                    {
                        SensorId = i,
                        Timestamp = t
                    })
                ));

            var runDaoMock = new Mock<IRunDao>();
            runDaoMock.Setup(d => d.GetCurrentRun())
                .Returns(Task.FromResult(
                    new Run
                    {
                        Race = new Race { NumberOfSensors = existingMeasurementsTimestamps.Length + 1 }
                    }
                ));

            var daoProvider = DaoProviderHelper.GetPartialDaoProvider(
                runDao: runDaoMock.Object,
                sensorMeasurementDao: sensorMeasurementDaoMock.Object
            );
            return new RunService(daoProvider, raceClock);
        }

        [Fact]
        public void TestHandleSensorImpulse()
        {
            // Setup
            var raceClock = new SimulatorRaceClock();
            var runService = GetRunServiceForSensorImpulseHandlingTest(raceClock);
            var eventTriggered = false;
            runService.SensorMeasurementAdded += (race, runNumber, skier, span) => eventTriggered = true;

            // Execute & Assert
            raceClock.SendImpulse(1); // invalid impulse
            Assert.False(eventTriggered);

            raceClock.SendImpulse(3);
            Assert.True(eventTriggered);
        }

        [Fact]
        public void TestLeaderBoardUpdatedEventInvocation()
        {
            // Setup
            var raceClock = new SimulatorRaceClock();
            var runService = GetRunServiceForSensorImpulseHandlingTest(raceClock);
            var eventTriggered = false;
            runService.LeaderBoardUpdated += (race, number, runs) => eventTriggered = true;

            // Execute
            raceClock.SendImpulse(3);

            // Assert
            Assert.True(eventTriggered);
        }
    }
}