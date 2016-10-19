using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class Position : IEntity {
        public string Description { get; set; }
        public string PositionName { get; set; }
        public float HourlyFee { get; set; }

        public List<string> ReturnValues() {
            return new List<string> {Description, PositionName, HourlyFee.ToString()};
        }
    }
}