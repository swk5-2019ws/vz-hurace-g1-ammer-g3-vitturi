using System.Collections.Generic;
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

        public async Task<IEnumerable<Race>> SearchRaces(string nameSubstring)
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
    }
}