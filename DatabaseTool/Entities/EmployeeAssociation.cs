using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class EmployeeAssociation : IEntity{
        public int Bsn { get; set; }
        public string PositionName { get; set; }
        public int ProjectId { get; set; }
        public int WorkingHours { get; set; }
        public List<string> ReturnValues() {
            return new List<string> { Bsn.ToString(), PositionName, ProjectId.ToString(), WorkingHours.ToString()};
        }
    }
}
