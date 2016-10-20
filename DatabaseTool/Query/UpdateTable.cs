using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseTool.Connector;
using DatabaseTool.Entities;

namespace DatabaseTool.Query {
    public static class UpdateTable {
        public static void UpdateEmployee(DatabaseConnection connection, Employee employee, string newName, string newSurname, string newBuildingName) {
            connection.Connection.Open();
            string baseQuery = "UPDATE employee e SET ";
            string selectedEmployee = $"WHERE bsn = '{employee.Bsn.ToString("000000000")}'";
            var command = connection.Connection.CreateCommand();
            baseQuery += $"e.name = '{newName}', e.surname = '{newSurname}', e.building_name = '{newBuildingName}' ";
            employee.Name = newName;
            employee.Surname = newSurname;
            employee.MainBuildingName = newBuildingName;

            baseQuery += selectedEmployee;
            command.CommandText = baseQuery;
            command.ExecuteNonQuery();
            connection.Connection.Close();
        }
    }
}
