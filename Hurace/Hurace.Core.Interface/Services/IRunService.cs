using Hurace.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Interface.Services
{
    public delegate void LeaderBoardUpdateHandler(Race race, int runNumber, IEnumerable<Run> runs);

    public delegate void SensorMeasurementAddedHandler(Race race, int runNumber, Skier skier, TimeSpan timeSpan);

    public delegate void RunStatusChangedHandler(Race race, int runNumber, Skier skier, RunStatus runStatus);

    public interface IRunService
    {
        public event SensorMeasurementAddedHandler SensorMeasurementAdded;

        public event RunStatusChangedHandler RunStatusChanged;

        public event LeaderBoardUpdateHandler LeaderBoardUpdated;

        Task UpdateRunStatus(Race race, int runNumber, Skier skier, RunStatus status);

        Task<IList<TimeSpan>> GetInterimTimes(Race race, int runNumber, Skier skier);

        Task<IEnumerable<Run>> GetAllRunsForRace(Race race, int runNumber);

        Task<Run> GetCurrentRun();

        Task<IEnumerable<Run>> GetLeaderBoard(Race race, int runNumber);
    }
}
