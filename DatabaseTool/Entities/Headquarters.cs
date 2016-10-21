using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class Headquarters : IEntity{
        public string BuildingName { get; set; }
        public float Rent { get; set; }
        public int Rooms { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
