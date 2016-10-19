using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class Employee : IEntity{
        public int Bsn { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MainBuildingName { get; set; }
        public List<string> ReturnValues() {
            return new List<string> { Bsn.ToString(), Name, Surname, MainBuildingName};
        }
    }
}
