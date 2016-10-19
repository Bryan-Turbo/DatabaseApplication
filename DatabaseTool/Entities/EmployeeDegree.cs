using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class EmployeeDegree : IEntity{
        public int Bsn { get; set; }
        public string Course { get; set; }
        public List<string> ReturnValues() {
            return new List<string> { Bsn.ToString(), Course};
        }
    }
}
