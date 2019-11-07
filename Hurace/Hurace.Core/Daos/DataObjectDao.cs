using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Core.Interface;
using Hurace.Core.Mapper;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public abstract class DataObjectDao<T> : IDataObjectDao<T> where T : DataObject
    {
        protected DataObjectDao(ConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        protected ConnectionFactory ConnectionFactory { get; set; }

        public Task<IEnumerable<T>> FindAll()
        {
            using var connection = ConnectionFactory.CreateConnection();
            return connection.GetAll<T>();
        }

        public Task<T> FindById(int id)
        {
            using var connection = ConnectionFactory.CreateConnection();
            return connection.Get<T>(id);
        }

        public Task<bool> Update(T dataObject)
        {
            using var connection = ConnectionFactory.CreateConnection();
            return connection.Update(dataObject);
        }

        public Task<T> Insert(T dataObject)
        {
            using var connection = ConnectionFactory.CreateConnection();
            return connection.Insert(dataObject);
        }

        public Task<bool> Delete(int id)
        {
            using var connection = ConnectionFactory.CreateConnection();
            return connection.Delete<T>(id);
        }
    }
}