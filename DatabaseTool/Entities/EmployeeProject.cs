using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class EmployeeProject : IEntity{
        public int Bsn { get; set; }
        public string PositionName { get; set; }
        public int ProjectId { get; set; }
        public int WorkingHours { get; set; }
    }
}
