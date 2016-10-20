using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseTool.Connector;

namespace DatabaseTool.Query {
    public static class DeleteFromTable {
        public static void DeleteEmployee(DatabaseConnection connection, int employeeBsn) {
            connection.Connection.Open();
            var command = connection.Connection.CreateCommand();
            command.CommandText = $"DELETE FROM employee WHERE bsn = {employeeBsn}";
            command.ExecuteNonQuery();
            connection.Connection.Close();
        }
    }
}
