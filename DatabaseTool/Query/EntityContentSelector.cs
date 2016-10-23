using System.Collections.Generic;
using DatabaseTool.Entities;
using DatabaseTool.Connector;
using MySql.Data.MySqlClient;

namespace DatabaseTool.Query {
    public static class EntityContentSelector {
        public static List<Address> SelectAddress() {
            List<Address> entityList = new List<Address>();
            ConnectionHolder.Connection.Connection.Open();
            int houseNumber;
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
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
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }

        public static List<Employee> SelectEmployee() {
            List<Employee> entityList = new List<Employee>();
            ConnectionHolder.Connection.Connection.Open();
            int bsn;
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
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
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }

        public static List<Headquarters> SelectHeadquarters() {
            List<Headquarters> entityList = new List<Headquarters>();
            ConnectionHolder.Connection.Connection.Open();
            int rent, rooms;
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
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
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }

        public static List<Degree> SelectDegree() {
            List<Degree> entityList = new List<Degree>();
            ConnectionHolder.Connection.Connection.Open();
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM degree";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                entityList.Add(new Degree {
                    Course = reader["course"].ToString(),
                    DegreeLevel = reader["degree_level"].ToString(),
                    School = reader["school"].ToString()
                });
            }
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }

        public static List<Position> SelectPosition() {
            List<Position> entityList = new List<Position>();
            ConnectionHolder.Connection.Connection.Open();
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
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
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }

        public static List<Project> SelectProject() {
            List<Project> entityList = new List<Project>();
            ConnectionHolder.Connection.Connection.Open();
            int projectId, totalHours;
            float budget;
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
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
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }

        public static List<EmployeeAddress> SelectEmployeeAddress() {
            List<EmployeeAddress> entityList = new List<EmployeeAddress>();
            ConnectionHolder.Connection.Connection.Open();
            int bsn;
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
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
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }

        public static List<EmployeePosition> SelectEmployeePosition() {
            List<EmployeePosition> entityList = new List<EmployeePosition>();
            ConnectionHolder.Connection.Connection.Open();
            int bsn;
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM employee_position";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["bsn"].ToString(), out bsn);
                entityList.Add(new EmployeePosition {
                    Bsn = bsn,
                    PositionName = reader["position_name"].ToString()
                });
            }
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }

        public static List<EmployeeProject> SelectEmployeeProject() {
            List<EmployeeProject> entityList = new List<EmployeeProject>();
            ConnectionHolder.Connection.Connection.Open();
            int bsn, projectId, workingHours;
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM employee_project";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["bsn"].ToString(), out bsn);
                int.TryParse(reader["project_id"].ToString(), out projectId);
                int.TryParse(reader["working_hours"].ToString(), out workingHours);
                entityList.Add(new EmployeeProject {
                    Bsn = bsn,
                    PositionName = reader["position_name"].ToString(),
                    ProjectId = projectId,
                    WorkingHours = workingHours
                });
            }
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }

        public static List<EmployeeDegree> SelectEmployeeDegree() {
            List<EmployeeDegree> entityList = new List<EmployeeDegree>();
            ConnectionHolder.Connection.Connection.Open();
            int bsn;
            MySqlCommand command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM employee_degree";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                int.TryParse(reader["bsn"].ToString(), out bsn);
                entityList.Add(new EmployeeDegree {
                    Bsn = bsn,
                    Course = reader["course"].ToString()
                });
            }
            ConnectionHolder.Connection.Connection.Close();
            return entityList;
        }
    }
}