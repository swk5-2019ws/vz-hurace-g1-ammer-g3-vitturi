using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Hurace.Core.Mapper
{
    public static class MapperExtensions
    {
        /// <summary>
        /// Returns a list of entities.  
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <returns>Entities of Type TEntity</returns>
        public static Task<IEnumerable<TEntity>> GetAll<TEntity>(this DbConnection connection)
        {
            return null;
        }

        /// <summary>
        /// Returns a single entity by a single id.  
        /// Id must be marked with [Key] attribute.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="id">Id of the entity to get, must be marked with [Key] attribute</param>
        /// <returns>Entity of Type TEntity</returns>
        public static Task<TEntity> Get<TEntity>(this DbConnection connection, dynamic id)
        {
            return null;
        }

        /// <summary>
        /// Inserts an entity and returns an id.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entity">The new entity.</param>
        /// <returns>Id of inserted entity</returns>
        public static Task<object> Insert<TEntity>(this DbConnection connection, TEntity entity)
        {
            return null;
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entity">The updated entity.</param>
        /// <returns>True if updated. Otherwise false.</returns>
        public static Task<bool> Update<TEntity>(this DbConnection connection, TEntity entity)
        {
            return null;
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="id">Id of the entity to delete, must be marked with [Key] attribute</param>
        /// <returns>True if deleted. Otherwise false if not found.</returns>
        public static Task<bool> Delete<TEntity>(this DbConnection connection, dynamic id)
        {
            return null;
        }
    }
}
