﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hurace.Core.Interface;
using Hurace.Core.Mapper;
using Hurace.Domain;

#nullable enable
namespace Hurace.Core.Daos
{
    public class SkierDao : DataObjectDao<Skier>, ISkierDao
    {
        public SkierDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<Skier>> SearchSkiers(string? nameSubstring = null, Country? country = null,
            Gender? gender = null)
        {
            using var connection = ConnectionFactory.CreateConnection();
            var query = new StringBuilder("SELECT * FROM skier ");

            if (nameSubstring != null || country != null || gender != null)
            {
                query.Append("WHERE ");
                bool multipleFilters = false;

                if (nameSubstring != null)
                {
                    query.Append("(first_name || ' ' || last_name) LIKE ('%' || @NameSubstring || '%') ");
                    multipleFilters = true;
                }

                if (country != null)
                {
                    if (multipleFilters) query.Append("AND ");
                    query.Append("country_id = @CountryId ");
                    multipleFilters = true;
                }

                if (gender != null)
                {
                    if (multipleFilters) query.Append("AND ");
                    query.Append("gender = @Gender ");
                }
            }

            return await connection.Query<Skier>(query.ToString(), new
                {
                    NameSubstring = nameSubstring,
                    CountryId = country?.Id,
                    Gender = gender,
                }
            ).ConfigureAwait(false);
        }
    }
}