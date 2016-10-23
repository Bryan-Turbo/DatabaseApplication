using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseTool.Connector;

namespace DatabaseTool.Query {
    public static class DeleteFromTable {
        public static void DeleteEmployee(int employeeBsn) {
            ConnectionHolder.Connection.Connection.Open();
            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText = $"DELETE FROM employee WHERE bsn = {employeeBsn}";
            command.ExecuteNonQuery();
            ConnectionHolder.Connection.Connection.Close();
        }

        public static void DeleteEmployeeAddress(int employeeBsn, string postalCode, string country) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"DELETE FROM employee_address WHERE bsn = '{employeeBsn}' AND postal_code = '{postalCode}' AND country = '{country}'";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void DeleteEmployeeDegree(int employeeBsn, string course) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"DELETE FROM employee_degree WHERE bsn = '{employeeBsn.ToString("000000000")}' AND course = '{course}'";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void DeleteEmployeePosition(int employeeBsn, string positionName) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"DELETE FROM employee_position WHERE bsn = '{employeeBsn.ToString("000000000")}' AND position_name = '{positionName}'";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void DeleteProject(int projectId) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText = $"DELETE FROM project WHERE project_id = '{projectId}'";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void DeleteEmployeeProject(int bsn, int projectId, string positionName) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"DELETE FROM employee_project WHERE project_id = '{projectId}' AND bsn = '{bsn}' AND position_name = '{positionName}'";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }
    }
}