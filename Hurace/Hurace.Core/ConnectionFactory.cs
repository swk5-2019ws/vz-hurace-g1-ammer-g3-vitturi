using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace Hurace.Core
{
    public class ConnectionFactory
    {
        public string ConnectionString { get; }

        public ConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DbConnection CreateConnection()
        {
            DbConnection connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            InitializeDatabase(connection);
            return connection;
        }

        private void InitializeDatabase(DbConnection connection)
        {
            using var command = connection.CreateCommand();
            string script = File.ReadAllText(".\\Scripts\\create_tables.sql");
            command.CommandText = script;
            command.ExecuteNonQuery();
        }
    }
}
