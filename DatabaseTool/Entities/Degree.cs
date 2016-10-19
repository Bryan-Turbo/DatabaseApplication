using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class Degree :IEntity{
        public string Course { get; set; }
        public string School { get; set; }
        public string DegreeLevel { get; set; }
        public List<string> ReturnValues() {
            return new List<string> { Course, School, DegreeLevel};
        }
    }
}
