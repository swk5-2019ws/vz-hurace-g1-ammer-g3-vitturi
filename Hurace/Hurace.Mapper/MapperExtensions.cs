using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
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
            var keyColumn = AttributeParser.GetColumnName(key);
            var sql = $"SELECT * FROM {tableName} WHERE {keyColumn} = @Id";

            return (await connection.Query<TEntity>(sql, new { Id = id }).ConfigureAwait(false)).FirstOrDefault();
        }

        /// <summary>
        ///     Inserts an entity and returns an id.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="entity">The new entity.</param>
        /// <returns>Id of inserted entity</returns>
        public static Task<int> Insert<TEntity>(this DbConnection connection, TEntity entity)
        {
            var type = typeof(TEntity);
            var name = AttributeParser.GetTableName(type);
            var properties = AttributeParser.GetAllProperties(type);
            var collections = AttributeParser.GetAllCollections(type);
            var key = AttributeParser.GetKey(type);

            if (AttributeParser.GetKeyIsGenerated(key))
            {
                properties = properties.Where(pi => pi != key);
            }
            var insertAttributes = properties.Except(collections).ToList();

            var columns = new StringBuilder(null);
            for (var i = 0; i < insertAttributes.Count; i++)
            {
                columns.Append(AttributeParser.GetColumnName(insertAttributes[i]));
                if (i < insertAttributes.Count - 1)
                    columns.Append(", ");
            }

            var parameters = new StringBuilder(null);
            for (var i = 0; i < insertAttributes.Count; i++)
            {
                parameters.Append($"@{insertAttributes[i].Name}");
                if (i < insertAttributes.Count - 1)
                    parameters.Append(", ");
            }

            var sql = $"INSERT INTO {name} ({columns}) VALUES ({parameters})";
            return connection.Execute(sql, entity);
        }

        /// <summary>
        ///     Updates an entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="entity">The updated entity.</param>
        /// <returns>True if updated. Otherwise false.</returns>
        public static async Task<bool> Update<TEntity>(this DbConnection connection, TEntity entity)
        {
            var type = typeof(TEntity);
            var name = AttributeParser.GetTableName(type);
            var properties = AttributeParser.GetAllProperties(type);
            var collections = AttributeParser.GetAllCollections(type);
            var key = AttributeParser.GetKey(type);
            var keyColumn = AttributeParser.GetColumnName(key);
            var updateAttributes = properties.Except(collections).Where(pi => pi != key).ToList();

            var sql = new StringBuilder($"UPDATE {name} SET");

            for (var i = 0; i < updateAttributes.Count; i++)
            {
                sql.Append($"{AttributeParser.GetColumnName(updateAttributes[i])} = @{updateAttributes[i].Name}");
                if (i < updateAttributes.Count - 1)
                    sql.Append(", ");
            }

            sql.Append($" WHERE {keyColumn} = @{key.Name}");

            var updated = await connection.Execute(sql.ToString(), entity).ConfigureAwait(false);
            return updated > 0;
        }

        /// <summary>
        ///     Deletes an entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="id">Id of the entity to delete, must be marked with [Key] attribute</param>
        /// <returns>True if deleted. Otherwise false if not found.</returns>
        public static async Task<bool> Delete<TEntity>(this DbConnection connection, dynamic id)
        {
            var type = typeof(TEntity);
            var tableName = AttributeParser.GetTableName(type);
            var key = AttributeParser.GetKey(type);
            var keyColumn = AttributeParser.GetColumnName(key);
            var sql = $"DELETE FROM {tableName} WHERE {keyColumn} = @Id";

            var deleted = await connection.Execute(sql, new {Id = id}).ConfigureAwait(false);
            return deleted > 0;
        }

        /// <summary>
        ///     Execute a query.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open DbConnection</param>
        /// <param name="sql">The SQL to execute</param>
        /// <param name="param">The parameters for the query</param>
        /// <returns>The entities.</returns>
        public static async Task<IEnumerable<TEntity>> Query<TEntity>(this DbConnection connection, string sql,
            object param = null)
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
        public static async Task<int> Execute(this DbConnection connection, string sql,
            object param = null)
        {
            _ = connection ?? throw new ArgumentNullException($"{nameof(connection)} must not be null!");

            using DbCommand command = connection.CreateCommand();
            command.CommandText = sql;

            if (param != null)
            {
                var properties = AttributeParser.GetAllProperties(param.GetType()).ToList();
                foreach (var propertyInfo in properties)
                {
                    object value = null;
                    if (AttributeParser.HasForeignKey(propertyInfo))
                    {
                        value = AttributeParser.GetKey(propertyInfo.PropertyType).GetValue(propertyInfo.GetValue(param));
                    } else if(propertyInfo.PropertyType.IsEnum)
                    {
                        value = propertyInfo.GetValue(param).ToString();
                    }

                    command.AddValue(propertyInfo.Name, value ?? propertyInfo.GetValue(param));
                }
            }
            
            return await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}