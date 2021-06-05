using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models
{
    public class AdminVM
    {
        public IEnumerable<Chemical> chemicals { get; set; }

        public IEnumerable<StatusTime> times { get; set; }

        public Company company { get; set; }

        public IEnumerable<Company> companies {get; set;}

        public string name {get; set;}

        public string access {get; set;}

        public int numFac {get; set;}
    }
}
