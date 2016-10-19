using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class EmployeeAdress : IEntity{
        public int Bsn { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsResidence { get; set; }
        public List<string> ReturnValues() {
            return new List<string> { Bsn.ToString(), Country, PostalCode, IsResidence.ToString()};
        }
    }
}
