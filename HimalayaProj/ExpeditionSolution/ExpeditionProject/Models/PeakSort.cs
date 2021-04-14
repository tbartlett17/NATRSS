using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpeditionProject.Models
{
    public class PeakSort
    {
        public IEnumerable<Expedition> expeditions { get; set; }

        public IEnumerable<Peak> peaks { get; set; }

        public IEnumerable<TrekkingAgency> agency { get; set; }

        public Peak mtnPeak { get; set; }
    }
}
