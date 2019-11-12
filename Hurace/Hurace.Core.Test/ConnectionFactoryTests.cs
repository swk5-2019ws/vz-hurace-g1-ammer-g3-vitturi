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
            Assert.Equal(ConnectionState.Open, connectionFactory.CreateConnection().State);
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
