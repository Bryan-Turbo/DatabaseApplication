using System.Data.SQLite;

namespace DatabaseTool.Connector {
    public interface IDatabaseConnection {
        SQLiteConnection Connection { get; }
        string Database { get; }
    }
}
