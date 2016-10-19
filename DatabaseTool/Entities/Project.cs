using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class Project : IEntity {
        public float Budget { get; set; }
        public int ProjectId { get; set; }
        public int TotalHours { get; set; }
        public string BuildingName { get; set; }

        public List<string> ReturnValues() {
            return new List<string> {this.Budget.ToString(), this.ProjectId.ToString(), this.TotalHours.ToString(), this.BuildingName};
        }
    }
}