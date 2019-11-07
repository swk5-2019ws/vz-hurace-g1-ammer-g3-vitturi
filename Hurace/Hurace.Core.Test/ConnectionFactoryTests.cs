using System.Data;
using System.Data.Common;
using Xunit;

namespace Hurace.Core.Test
{
    public class ConnectionFactoryTests
    {
        [Fact]
        public void GetConnection_NewConnection_IsOpen()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            Assert.Equal(ConnectionState.Open, connectionFactory.GetConnection().State);
        }

        [Fact]
        public void GetConnection_DifferentFactories_ReturnSameConnection()
        {
            ConnectionFactory connectionFactory1 = new ConnectionFactory(Environment.Testing);
            ConnectionFactory connectionFactory2 = new ConnectionFactory(Environment.Testing);
            DbConnection connection1 = connectionFactory1.GetConnection();
            DbConnection connection2 = connectionFactory2.GetConnection();
            Assert.Equal(connection1, connection2);
        }

        [Fact]
        public void CreateConnection_NewConnections_CreateDifferentConnections()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            DbConnection connection1 = connectionFactory.CreateConnection();
            DbConnection connection2 = connectionFactory.CreateConnection();
            Assert.NotEqual(connection1, connection2);
        }

        [Fact]
        public void GetConnection_ClosedConnection_ReopenConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            DbConnection connection = connectionFactory.GetConnection();
            Assert.Equal(ConnectionState.Open, connection.State);
            connection.Close();
            Assert.Equal(ConnectionState.Closed, connection.State);
            connectionFactory.GetConnection();
            Assert.Equal(ConnectionState.Open, connection.State);
        }

        [Fact]
        public void CreateConnection_NewDatabase_InitializeDatabase()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            DbConnection connection = connectionFactory.CreateConnection();
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * from skiers";
                command.ExecuteReader();
            }
        }
    }
}
