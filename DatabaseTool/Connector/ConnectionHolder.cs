using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Connector {
    public static class ConnectionHolder {
        public static DatabaseConnection Connection;

        public static void SetConnection(string databaseName) {
            Connection = new DatabaseConnection(databaseName);
        }
    }
}
