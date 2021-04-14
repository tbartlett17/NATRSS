using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpeditionProject.Models
{
    public class statsVM
    {
        public int currentNumberOfExpeditions { get; set; }
        public int currentNumberOfPeaks { get; set; }

        public int numberOfUnClimbedPeaks { get; set; }

        public List<string> mostPopularPeaks { get; set; }
        public List<string> peaksHigherThan7000 { get; set; }

        public List<string> peaksHigherThan6000 { get; set; }

        
    }
}
