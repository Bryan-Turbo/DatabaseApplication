using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTool.Entities {
    public class Address : IEntity{
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public List<string> ReturnValues() {
            return new List<string> { PostalCode, Country, City, Street, HouseNumber.ToString()};
        }
    }
}
