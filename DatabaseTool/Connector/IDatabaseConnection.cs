using MySql.Data.MySqlClient;

namespace DatabaseTool.Connector {
    public interface IDatabaseConnection {
        MySqlConnection Connection { get; }
        string Database { get; }
        //void InsertIntoTable(string postal, string country, string city, string street, string housenumber);
    }
}
