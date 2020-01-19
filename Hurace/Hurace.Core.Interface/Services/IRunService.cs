using Hurace.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Interface.Services
{
    public delegate void LeaderBoardUpdateHandler(Race race, int runNumber, IEnumerable<Run> runs);

    public delegate void SensorMeasurementAddedHandler(Race race, int runNumber, Skier skier, TimeSpan timeSpan);

    public delegate void RunStartedHandler(Race race, int runNumber, Skier skier);

    public delegate void RunStatusChangedHandler(Race race, int runNumber, Skier skier, RunStatus runStatus);

    public interface IRunService
    {
        event SensorMeasurementAddedHandler SensorMeasurementAdded;

        event RunStatusChangedHandler RunStatusChanged;

        event LeaderBoardUpdateHandler LeaderBoardUpdated;

        event RunStartedHandler RunStarted;

        Task UpdateRunStatus(Race race, int runNumber, Skier skier, RunStatus status);

        Task<IList<TimeSpan>> GetInterimTimes(Race race, int runNumber, Skier skier);

        Task<IList<TimeSpan>> GetInterimTimesDifferences(IList<TimeSpan> interimTimes, Race race, int runNumber, Skier skier);

        Task<IEnumerable<Run>> GetAllRunsForRace(Race race, int runNumber);

        Task<Run> GetCurrentRun();

        Task<IEnumerable<Run>> GetLeaderBoard(Race race, int runNumber);
        
        Task<int> GetAmountOfRuns();
        
        Task<Run> GetRun(int id);
        
        Task<IEnumerable<Run>> GetRunsForSkierInSeasons(int skierId, uint season);
    }
}
