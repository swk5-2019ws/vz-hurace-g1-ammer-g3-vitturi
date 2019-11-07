using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace Hurace.Core
{
    public class ConnectionFactory
    {
        public string ConnectionString { get; }
        public Environment Environment { get; }

        private static readonly Dictionary<Environment, DbConnection> connections = new Dictionary<Environment, DbConnection>();

        public ConnectionFactory(Environment environment)
        {
            Environment = environment;
            ConnectionString = ConfigurationReader.GetConnectionString(environment);
        }

        public DbConnection GetConnection()
        {
            if (!connections.ContainsKey(Environment))
            {
                connections.Add(Environment, CreateConnection());
            }
            else if (connections[Environment].State == ConnectionState.Closed)
            {
                connections[Environment].Open();
            }

            return connections[Environment];
        }

        public DbConnection CreateConnection()
        {
            DbConnection connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            InitializeDatabase(connection);
            return connection;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Script does not contain user input")]
        private static void InitializeDatabase(DbConnection connection)
        {
            string script = File.ReadAllText(".\\Scripts\\create_tables.sql");

            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }
    }
}
