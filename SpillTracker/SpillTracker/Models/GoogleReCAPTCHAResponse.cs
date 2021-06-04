using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models
{
    public class GoogleReCAPTCHAResponse
    {
        public bool Success { get; set; }
        public double Score { get; set; }
        public string Action { get; set; }
        public DateTime Challenge_ts { get; set; }
        public string Hostname { get; set; }
    }
}
