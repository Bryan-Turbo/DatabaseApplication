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
    }
}