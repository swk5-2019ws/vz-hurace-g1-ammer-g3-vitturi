using System.Data;
using System.Data.Common;
using Xunit;

namespace Hurace.Core.Test
{
    public class ConnectionFactoryTests
    {
        [Fact]
        public void CreateConnection_NewConnection_IsOpen()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            Assert.Equal(ConnectionState.Open, connectionFactory.CreateConnection().State);
        }

        [Fact]
        public void CreateConnection_NewDatabase_InitializeDatabase()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            DbConnection connection = connectionFactory.CreateConnection();
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * from skier";
                command.ExecuteReader();
            }
        }

        [Fact]
        public void CreateConnection_InMemoryDatabase_IsPersistent()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            using (DbConnection connection = connectionFactory.CreateConnection())
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS new_table (id INTEGER)";
                command.ExecuteNonQuery();
            }

            using (DbConnection connection = connectionFactory.CreateConnection())
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM new_table";
                command.ExecuteReader();
            }
        }
    }
}
