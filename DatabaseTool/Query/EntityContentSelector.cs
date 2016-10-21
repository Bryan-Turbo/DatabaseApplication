using System.Collections.Generic;
using DatabaseTool.Entities;
using DatabaseTool.Connector;
using MySql.Data.MySqlClient;

namespace DatabaseTool.Query {
    public static class EntityContentSelector {
        public static List<Address> SelectAddress(DatabaseConnection connection) {
            List<Address> entityList = new List<Address>();
            connection.Connection.Open();
            int houseNumber;
            MySqlCommand command = connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM address";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["house_number"].ToString(), out houseNumber);
                entityList.Add(new Address {
                    PostalCode = reader["postal_code"].ToString(),
                    Country = reader["country"].ToString(),
                    City = reader["city"].ToString(),
                    Street = reader["street"].ToString(),
                    HouseNumber = houseNumber
                });
            }
            connection.Connection.Close();
            return entityList;
        }

        public static List<Employee> SelectEmployee(DatabaseConnection connection) {
            List<Employee> entityList = new List<Employee>();
            connection.Connection.Open();
            int bsn;
            MySqlCommand command = connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM employee";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["bsn"].ToString(), out bsn);
                entityList.Add(new Employee {
                    Bsn = bsn,
                    Name = reader["name"].ToString(),
                    Surname = reader["surname"].ToString(),
                    MainBuildingName = reader["building_name"].ToString()
                });
            }
            connection.Connection.Close();
            return entityList;
        }

        public static List<Headquarters> SelectHeadquarters(DatabaseConnection connection) {
            List<Headquarters> entityList = new List<Headquarters>();
            connection.Connection.Open();
            int rent, rooms;
            MySqlCommand command = connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM headquarters";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["rent"].ToString(), out rent);
                int.TryParse(reader["rooms"].ToString(), out rooms);
                entityList.Add(new Headquarters {
                    BuildingName = reader["building_name"].ToString(),
                    Rent = rent,
                    Rooms = rooms,
                    PostalCode = reader["postal_code"].ToString(),
                    Country = reader["country"].ToString()
                });
            }
            connection.Connection.Close();
            return entityList;
        }

        public static List<Degree> SelectDegree(DatabaseConnection connection) {
            List<Degree> entityList = new List<Degree>();
            connection.Connection.Open();
            MySqlCommand command = connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM degree";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                entityList.Add(new Degree {
                    Course = reader["course"].ToString(),
                    DegreeLevel = reader["degree_level"].ToString(),
                    School = reader["school"].ToString()
                });
            }
            connection.Connection.Close();
            return entityList;
        }

        public static List<Position> SelectPosition(DatabaseConnection connection) {
            List<Position> entityList = new List<Position>();
            connection.Connection.Open();
            MySqlCommand command = connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM position";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                float hourlyFee;
                float.TryParse(reader["hourly_fee"].ToString(), out hourlyFee);
                entityList.Add(new Position {
                    PositionName = reader["position_name"].ToString(),
                    Description = reader["description"].ToString(),
                    HourlyFee = hourlyFee
                });
            }
            connection.Connection.Close();
            return entityList;
        }

        public static List<Project> SelectProject(DatabaseConnection connection) {
            List<Project> entityList = new List<Project>();
            connection.Connection.Open();
            int projectId, totalHours;
            float budget;
            MySqlCommand command = connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM project";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["project_id"].ToString(), out projectId);
                int.TryParse(reader["total_hours"].ToString(), out totalHours);
                float.TryParse(reader["budget"].ToString(), out budget);
                entityList.Add(new Project {
                    ProjectId = projectId,
                    Budget = budget,
                    TotalHours = totalHours,
                    BuildingName = reader["building_name"].ToString()
                });
            }
            connection.Connection.Close();
            return entityList;
        }

        public static List<EmployeeAddress> SelectEmployeeAddress(DatabaseConnection connection) {
            List<EmployeeAddress> entityList = new List<EmployeeAddress>();
            connection.Connection.Open();
            int bsn;
            MySqlCommand command = connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM employee_address";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["bsn"].ToString(), out bsn);
                entityList.Add(new EmployeeAddress {
                    Bsn = bsn,
                    PostalCode = reader["postal_code"].ToString(),
                    Country = reader["country"].ToString(),
                    IsResidence = reader["is_residence"].ToString() == "1"
                });
            }
            connection.Connection.Close();
            return entityList;
        }

        public static List<EmployeeAssociation> SelectEmployeeAssociation(DatabaseConnection connection) {
            List<EmployeeAssociation> entityList = new List<EmployeeAssociation>();
            connection.Connection.Open();
            int bsn, projectId, workingHours;
            MySqlCommand command = connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM employee_association";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["bsn"].ToString(), out bsn);
                int.TryParse(reader["project_id"].ToString(), out projectId);
                int.TryParse(reader["working_hours"].ToString(), out workingHours);
                entityList.Add(new EmployeeAssociation {
                    Bsn = bsn,
                    PositionName = reader["position_name"].ToString(),
                    ProjectId = projectId,
                    WorkingHours = workingHours
                });
            }
            connection.Connection.Close();
            return entityList;
        }

        public static List<EmployeeDegree> SelectEmployeeDegree(DatabaseConnection connection) {
            List<EmployeeDegree> entityList = new List<EmployeeDegree>();
            connection.Connection.Open();
            int bsn;
            MySqlCommand command = connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM employee_degree";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["bsn"].ToString(), out bsn);
                entityList.Add(new EmployeeDegree {
                    Bsn = bsn,
                    Course = reader["course"].ToString()
                });
            }
            connection.Connection.Close();
            return entityList;
        }
    }
}