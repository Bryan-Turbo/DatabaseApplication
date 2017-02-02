using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.IO;
using System.Threading;
using System.Windows;
using DatabaseTool.Entities;

namespace DatabaseTool.Connector {
    public class DatabaseConnection : IDatabaseConnection {
        private SQLiteConnection _connection;

        public SQLiteConnection Connection {
            get { return this._connection; }
        }

        public string Database {
            get { return this._connection.Database; }
        }

        public DatabaseConnection(string databaseName) {
            bool newFile = !File.Exists($"{databaseName}.db");

            string connectionString = $"Data Source = {databaseName}.db";
            this._connection = new SQLiteConnection(connectionString);

            CheckNewFile(newFile, databaseName);
        }

        private void CheckNewFile(bool newFile, string databaseName) {
            if (!newFile)
                return;

            SQLiteConnection.CreateFile($"{databaseName}.db");

            string[] dbconfiglines = File.ReadAllLines("Content/database_config.sql");
            string config = "";
            foreach (string line in dbconfiglines) {
                config += line;
            }

            string[] dbDatalines = File.ReadAllLines("Content/database_standard_data.sql");
            string dbData = "";
            foreach (string line in dbDatalines) {
                dbData += line;
            }

            this._connection.Open();
            var command = this._connection.CreateCommand();
            command.CommandText = config;
            command.ExecuteNonQuery();
            command.CommandText = dbData;
            command.ExecuteNonQuery();
            this._connection.Close();
        }
    }
}