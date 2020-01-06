using Hurace.Core.Interface.Daos;
using Hurace.Core.Mapper;
using Hurace.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hurace.Core.Daos
{
    public class RunDao : DataObjectDao<Run>, IRunDao
    {
        public RunDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<Run> GetBySkierAndRace(Race race, int runNumber, Skier skier)
        {
            using var connection = ConnectionFactory.CreateConnection();
            var matchingRuns = await connection.Query<Run>(@"
                SELECT * FROM run
                WHERE skier_id = @SkierId
                AND race_id = @RaceId
                AND run_number = @RunNumber",
                new
                {
                    SkierId = skier.Id,
                    RaceId = race.Id,
                    RunNumber = runNumber
                }
            ).ConfigureAwait(false);
            return matchingRuns.FirstOrDefault();
        }

        public async Task<IEnumerable<Run>> GetAllRunsForRace(Race race, int runNumber)
        {
            using var connection = ConnectionFactory.CreateConnection();
            return await connection.Query<Run>(@"
                SELECT * FROM run
                WHERE race_id = @RaceId
                AND run_number = @RunNumber",
                new { RaceId = race.Id, RunNumber = runNumber }
            ).ConfigureAwait(false);
        }

        public async Task DeleteAllRunsForRace(Race race, int runNumber)
        {
            var runs = await GetAllRunsForRace(race, runNumber).ConfigureAwait(false);

            using var connection = ConnectionFactory.CreateConnection();
            var transaction = connection.BeginTransaction();
            foreach (var run in runs)
            {
                await connection.Delete<Run>(run.Id).ConfigureAwait(false);
            }

            transaction.Commit();
        }

        public async Task<Run> GetCurrentRun()
        {
            using var connection = ConnectionFactory.CreateConnection();
            var currentRuns = await connection.Query<Run>(@"
                SELECT * FROM run
                WHERE status = @Status",
                new { Status = RunStatus.InProgress }
            ).ConfigureAwait(false);
            return currentRuns.First();
        }

        public async Task<int> GetAmountOfRuns()
        {
            using var connection = ConnectionFactory.CreateConnection();
            var amountOfRuns = await connection.Query<int>("SELECT COUNT(*) FROM run").ConfigureAwait(false);
            return amountOfRuns.Any() ? amountOfRuns.First() : 0;
        }
    }
}