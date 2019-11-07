using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Hurace.Core.Mapper
{
    public static class MapperExtensions
    {
        /// <summary>
        ///     Returns a list of entities.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <returns>Entities of Type TEntity</returns>
        public static Task<IEnumerable<TEntity>> GetAll<TEntity>(this DbConnection connection)
        {
            var type = typeof(TEntity);
            var tableName = AttributeParser.GetTableName(type);
            var sql = $"SELECT * FROM {tableName}";

            return connection.Query<TEntity>(sql);
        }

        /// <summary>
        ///     Returns a single entity by a single id.
        ///     Id must be marked with [Key] attribute.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="id">Id of the entity to get, must be marked with [Key] attribute</param>
        /// <returns>Entity of Type TEntity</returns>
        public static async Task<TEntity> Get<TEntity>(this DbConnection connection, dynamic id)
        {
            var type = typeof(TEntity);
            var tableName = AttributeParser.GetTableName(type);
            var key = AttributeParser.GetKey(type);
            var sql = $"SELECT * FROM {tableName} WHERE {key} = @id";

            IDictionary<string, object> param = new Dictionary<string, object>();
            param["@id"] = id;

            return (await connection.Query<TEntity>(sql, param)).FirstOrDefault();
        }

        /// <summary>
        ///     Inserts an entity and returns an id.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="entity">The new entity.</param>
        /// <returns>Id of inserted entity</returns>
        public static Task<TEntity> Insert<TEntity>(this DbConnection connection, TEntity entity)
        {
            return null;
        }

        /// <summary>
        ///     Updates an entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="entity">The updated entity.</param>
        /// <returns>True if updated. Otherwise false.</returns>
        public static Task<bool> Update<TEntity>(this DbConnection connection, TEntity entity)
        {
            return null;
        }

        /// <summary>
        ///     Deletes an entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="id">Id of the entity to delete, must be marked with [Key] attribute</param>
        /// <returns>True if deleted. Otherwise false if not found.</returns>
        public static Task<bool> Delete<TEntity>(this DbConnection connection, dynamic id)
        {
            return null;
        }

        /// <summary>
        ///     Execute a query.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="sql">The SQL to execute</param>
        /// <param name="param">The parameters for the query</param>
        /// <returns>The entities.</returns>
        public static async Task<IEnumerable<TEntity>> Query<TEntity>(this IDbConnection connection, string sql,
            IDictionary<string, object> param = null)
        {
            return null;
        }

        /// <summary>
        ///     Execute a command.
        /// </summary>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="sql">The SQL to execute</param>
        /// <param name="param">The parameters for the query</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> ExecuteAsync(this IDbConnection connection, string sql,
            IDictionary<string, object> param = null)
        {
            return 0;
        }
    }
}