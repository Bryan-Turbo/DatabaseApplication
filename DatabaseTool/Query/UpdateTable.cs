using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseTool.Connector;
using DatabaseTool.Entities;

namespace DatabaseTool.Query {
    public static class UpdateTable {
        public static void UpdateEmployee(Employee employee, string newName, string newSurname, string newBuildingName) {
            ConnectionHolder.Connection.Connection.Open();
            string baseQuery = "UPDATE employee SET ";
            string selectedEmployee = $"WHERE bsn = '{employee.Bsn.ToString("000000000")}'";
            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            baseQuery += $"name = '{newName}', surname = '{newSurname}', building_name = '{newBuildingName}' ";
            employee.Name = newName;
            employee.Surname = newSurname;
            employee.MainBuildingName = newBuildingName;

            baseQuery += selectedEmployee;
            command.CommandText = baseQuery;
            command.ExecuteNonQuery();
            ConnectionHolder.Connection.Connection.Close();
        }

        public static void UpdateEmployeeAddress(EmployeeAddress oldEmployeeAddress, EmployeeAddress newEmployeeAddress) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();

            byte bit = 0;
            if (newEmployeeAddress.IsResidence) {
                bit = 1;
            }

            string baseQuery = "UPDATE employee_address ";

            string getCurrentEmployee =
                $"WHERE bsn = '{oldEmployeeAddress.Bsn}' AND postal_code = '{oldEmployeeAddress.PostalCode}' AND country = '{oldEmployeeAddress.Country}' ";
            string changeValues =
                $"SET bsn = '{newEmployeeAddress.Bsn}', postal_code = '{newEmployeeAddress.PostalCode}', country = '{newEmployeeAddress.Country}', is_residence = {bit} ";

            string query = baseQuery + changeValues + getCurrentEmployee;
            command.CommandText = query;
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void UpdateProject(int oldProjectId, float newBudget, float newTotalHours, string newBuildingName) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"UPDATE project SET budget = '{newBudget}', total_hours = '{newTotalHours}', building_name = '{newBuildingName}' WHERE project_id = '{oldProjectId}'";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }
    }
}