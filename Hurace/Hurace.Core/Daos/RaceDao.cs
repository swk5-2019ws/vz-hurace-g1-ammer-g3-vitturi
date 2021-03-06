﻿using Hurace.Core.Interface.Daos;
using Hurace.Core.Mapper;
using Hurace.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                new { NameSubstring = nameSubstring }
            ).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Race>> GetLastRaces(int count)
        {
            using var connection = ConnectionFactory.CreateConnection();
            return await connection.Query<Race>(@"
                SELECT * FROM race
                ORDER BY date DESC
                LIMIT @Count",
                new { Count = count }
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

        public async Task<int> GetAmountOfRaces()
        {
            using var connection = ConnectionFactory.CreateConnection();
            var amountOfRaces = await connection.Query<int>("SELECT COUNT(*) FROM race").ConfigureAwait(false);
            return amountOfRaces.Any() ? amountOfRaces.First() : 0;
        }
    }
}