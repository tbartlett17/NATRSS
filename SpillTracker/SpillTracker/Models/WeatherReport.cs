using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models
{
    public class WeatherReport
    {
        public DateTime DateTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Temperature { get; set; }
        public string TemperatureUnits { get; set; }
        public double Humidity { get; set; }
        public string HumidityUnits { get; set; }
        public double WindSpeed { get; set; }
        public string WindSpeedUnits { get; set; }
        public string WindDirection { get; set; }
        public string SkyConditions { get; set; }
    }
}
