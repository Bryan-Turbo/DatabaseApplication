using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows;
using DatabaseTool.Entities;

namespace DatabaseTool.Connector {
    public class DatabaseConnection : IDatabaseConnection {
        private MySqlConnection _connection;

        public MySqlConnection Connection {
            get { return this._connection; }
        }

        public string Database {
            get { return this._connection.Database; }
        }

        public DatabaseConnection(string server, string databaseName, string username, string password) {
            string connectionString = $"server={server}; database={databaseName}; UID={username}; password={password}";
            this._connection = new MySqlConnection(connectionString);
        }
    }
}