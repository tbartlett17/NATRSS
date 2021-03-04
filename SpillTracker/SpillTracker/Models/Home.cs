using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SpillTracker.Models
{
    public class Home {
        public IEnumerable<Chemical> chemicals {get; set;}

        public IEnumerable<StatusTime> times {get; set;}

    }
}