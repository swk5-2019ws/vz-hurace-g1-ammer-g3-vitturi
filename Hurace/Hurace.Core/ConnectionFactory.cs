using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace Hurace.Core
{
    public class ConnectionFactory
    {
        public string ConnectionString { get; private set; }
        public Environment Environment { get; private set; }

        private bool initialized;
        private DbConnection keepAliveConnection; // First connection to in-memory cached databases
        private string guid;
        private const string guid_placeholder = "{guid}";

        public ConnectionFactory(Environment environment)
        {
            this.Environment = environment;
            this.ConnectionString = ConfigurationReader.GetConnectionString(environment);
            if (ConnectionString.Contains(guid_placeholder))
            {
                this.guid = Guid.NewGuid().ToString();
                this.ConnectionString = this.ConnectionString.Replace(guid_placeholder, this.guid);
            }
            this.initialized = false;
        }

        public DbConnection CreateConnection()
        {
            DbConnection connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            if (!initialized)
            {
                InitializeDatabase(connection);
                initialized = true;
            }

            if (keepAliveConnection == null && this.guid != null)
            {
                keepAliveConnection = connection;
                return CreateConnection();
            }

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
