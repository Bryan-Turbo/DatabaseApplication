using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Connector {
    public static class ConnectionHolder {
        public static DatabaseConnection Connection = new DatabaseConnection("localhost", "assignment1", "root", "");
    }
}
