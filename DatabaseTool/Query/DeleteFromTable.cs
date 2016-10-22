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

        public static void DeleteEmployeeAddress(DatabaseConnection connection, int employeeBsn, string postalCode, string country) {
            connection.Connection.Open();

            var command = connection.Connection.CreateCommand();
            command.CommandText = $"DELETE FROM employee_address WHERE bsn = '{employeeBsn}' AND postal_code = '{postalCode}' AND country = '{country}'";
            command.ExecuteNonQuery();

            connection.Connection.Close();
        }

        public static void DeleteEmployeeDegree(DatabaseConnection connection, int employeeBsn, string course) {
            connection.Connection.Open();

            var command = connection.Connection.CreateCommand();
            command.CommandText = $"DELETE FROM employee_degree WHERE bsn = '{employeeBsn.ToString("000000000")}' AND course = '{course}'";
            command.ExecuteNonQuery();

            connection.Connection.Close();
        }

        public static void DeleteEmployeePosition(DatabaseConnection connection, int employeeBsn, string positionName) {
            connection.Connection.Open();

            var command = connection.Connection.CreateCommand();
            command.CommandText = $"DELETE FROM employee_position WHERE bsn = '{employeeBsn.ToString("000000000")}' AND position_name = '{positionName}'";
            command.ExecuteNonQuery();

            connection.Connection.Close();
        }

        public static void DeleteProject(DatabaseConnection connection, int projectId) {
            connection.Connection.Open();

            var command = connection.Connection.CreateCommand();
            command.CommandText = $"DELETE FROM project WHERE project_id = '{projectId}'";
            command.ExecuteNonQuery();

            connection.Connection.Close();
        }
    }
}
