using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models
{
    public class Commit
    {
        public string commitId { get; set; } // grab the sha string
        public string date { get; set; } // grab the date of the commit

        public string commitMessage { get; set; } // grab the message if one exists

        public string commitLink { get; set; }

        //public 
    }
}
