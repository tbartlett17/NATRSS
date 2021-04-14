using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpillTracker.Utilities
{
    public class BadEmailUtilities
    {
        public static double RegexDensityParse(string input)
        {
            double density = double.Parse(Regex.Match(input, @"^\d*\.*\d*").Value);
            return density;
        }
    }
}
