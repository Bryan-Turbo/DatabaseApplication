using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseTool.Connector;

namespace DatabaseTool.Query {
    public static class InsertIntoTable {
        public static void InsertEmployee(int bsn, string name, string surname, string mainHq) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO employee(bsn, name, surname, building_name) VALUES('{bsn.ToString("000000000")}', '{name}', '{surname}', '{mainHq}')";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void InsertEmployeeAddress(int bsn, string postalCode, string country, bool isResidence) {
            ConnectionHolder.Connection.Connection.Open();
            byte bit = 0;
            if (isResidence) bit = 1;
            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO employee_address(bsn, postal_code, country, is_residence) VALUES('{bsn.ToString("000000000")}', '{postalCode}', '{country}', {bit.ToString("0")})";
            command.ExecuteNonQuery();
            ConnectionHolder.Connection.Connection.Close();
        }

        public static void InsertEmployeeDegree(int bsn, string course) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO employee_degree(bsn, course) VALUES('{bsn.ToString("000000000")}', '{course}')";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void InsertEmployeePosition(int bsn, string positionName) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO employee_position(bsn, position_name) VALUES('{bsn.ToString("000000000")}', '{positionName}')";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void InsertProject(float budget, float totalHours, string buildingName) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO project(budget, total_hours, building_name) VALUES('{budget}', '{totalHours}', '{buildingName}')";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void InsertEmployeeProject(int bsn, int projectId, string positionName, int workingHours) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO employee_project(project_id, bsn, position_name, working_hours) VALUES ('{projectId}', '{bsn}', '{positionName}', '{workingHours}')";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }
    }
}