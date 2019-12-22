using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurace.Core.Interface;
using Hurace.Core.Mapper;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class RaceDao : DataObjectDao<Race>, IRaceDao
    {
        public RaceDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<Race>> FindByName(string nameSubstring)
        {
            using var connection = ConnectionFactory.CreateConnection();
            return await connection.Query<Race>(@"
                SELECT * FROM race
                WHERE name LIKE '%' || @NameSubstring || '%'",
                new {NameSubstring = nameSubstring}
            ).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Race>> GetLastRaces(int count)
        {
            using var connection = ConnectionFactory.CreateConnection();
            return await connection.Query<Race>(@"
                SELECT * FROM race
                ORDER BY date DESC
                LIMIT @Count",
                new {Count = count}
            ).ConfigureAwait(false);
        }

        public async Task<Race> GetCurrentRace()
        {
            using var connection = ConnectionFactory.CreateConnection();
            var currentRaces = await connection.Query<Race>(@"
                SELECT * FROM race
                WHERE status = @Status",
                new { Status = RaceStatus.InProgress }
            ).ConfigureAwait(false);
            return currentRaces.Any() ? currentRaces.First() : null;
        }
    }
}