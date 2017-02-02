using MySql.Data.MySqlClient;
using System.Data.SQLite;

namespace DatabaseTool.Connector {
    public interface IDatabaseConnection {
        SQLiteConnection Connection { get; }
        string Database { get; }
        //void InsertIntoTable(string postal, string country, string city, string street, string housenumber);
    }
}
