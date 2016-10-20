using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseTool.Connector;

namespace DatabaseTool.Query {
    public static class InsertIntoTable {
        public static void InsertEmployee(DatabaseConnection connection, int bsn, string name, string surname, string mainHq) {
            connection.Connection.Open();

            var command = connection.Connection.CreateCommand();
            command.CommandText = $"INSERT INTO employee(bsn, name, surname, building_name) VALUES('{bsn.ToString("000000000")}', '{name}', '{surname}', '{mainHq}')";
            command.ExecuteNonQuery();

            connection.Connection.Close();
        }

        public static void InsertEmployeeAdress(DatabaseConnection connection, int bsn, string postalCode, string country, bool isResidence) {
            
        }
    }
}
