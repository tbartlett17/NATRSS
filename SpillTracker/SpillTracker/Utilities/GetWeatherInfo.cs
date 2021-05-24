using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpillTracker.Models;


namespace SpillTracker.Utilities
{
    public class GetWeatherInfo
    {
        public static WeatherReport GetWeather(string coords, DateTime spillDateTime, string apiKey)
        {
            WeatherReport weatherReport = new WeatherReport();
            try
            {
                //ex coords format44.623521, -123.047074

                string coordsTrimmed = String.Concat(coords.Where(c => !Char.IsWhiteSpace(c)));
                string[] coordsArr = coordsTrimmed.Split(',');
                string _lat = coordsArr[0];
                string _long = coordsArr[1];

                //Debug.WriteLine($"date b4 subtract: {spillDateTime}");

                int unixSpillDateTime = (int)(spillDateTime.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds; //convert to unix time for API historical call

                //Debug.WriteLine($"subtract: {DateTime.UtcNow.Subtract(spillDateTime.ToUniversalTime()).TotalMinutes}");

                string currentWeatherDataCall = $"https://api.openweathermap.org/data/2.5/onecall?lat={_lat}&lon={_long}&exclude=minutely,daily,hourly,alerts&units=imperial&appid={apiKey}";
                string historicWeatherDataCall = $"https://api.openweathermap.org/data/2.5/onecall/timemachine?lat={_lat}&lon={_long}&dt={unixSpillDateTime}&units=imperial&appid={apiKey}";

                if (DateTime.UtcNow.Subtract(spillDateTime.ToUniversalTime()).TotalMinutes < 30 && DateTime.UtcNow.Subtract(spillDateTime.ToUniversalTime()).TotalMinutes > 0)// if spill occured within last 30 min, use current weather 
                {
                    Debug.WriteLine("getting current weather");
                    weatherReport = CallAPI(currentWeatherDataCall);
                }
                else if (DateTime.UtcNow.Subtract(spillDateTime.ToUniversalTime()).TotalMinutes >= 30 && DateTime.UtcNow.Subtract(spillDateTime.ToUniversalTime()).TotalHours <=(24*5))// if spill occured after last 30 min and within last 5 days 
                {
                    Debug.WriteLine("getting historic weather");
                    weatherReport = CallAPI(historicWeatherDataCall);
                }
                else // spill occured more than 5 days ago or in the future, limitation of API without paying money
                {
                    return null;
                }

                return weatherReport;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return null;
            }
        }

        private static WeatherReport CallAPI(string url)
        {
            OWMReport owmWeather = new OWMReport();
            WeatherReport weatherReport = new WeatherReport();

            // list of degrees and their cardinal directions 
            List<Tuple<int, string>> windDirections = new List<Tuple<int, string>>
                    {
                        new Tuple<int, string>(0, "N"),
                        new Tuple<int, string>(45/2, "NNE"),
                        new Tuple<int, string>(45, "NE"),
                        new Tuple<int, string>(90-(45/2), "ENE"),
                        new Tuple<int, string>(90, "E"),
                        new Tuple<int, string>(135-(45/2), "SES"),
                        new Tuple<int, string>(135, "SE"),
                        new Tuple<int, string>(180-(45/2), "SSE"),
                        new Tuple<int, string>(180, "S"),
                        new Tuple<int, string>(225-(45/2), "SSW"),
                        new Tuple<int, string>(225, "SW"),
                        new Tuple<int, string>(270-(45/2), "WSW"),
                        new Tuple<int, string>(270, "W"),
                        new Tuple<int, string>(315-(45/2), "WNW"),
                        new Tuple<int, string>(315, "NW"),
                        new Tuple<int, string>(360-(45/2), "NNW"),
                        new Tuple<int, string>(360, "N")
                    };

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                string jsonResult = null;
                // TODO: You should handle exceptions here
                using (WebResponse response = request.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    jsonResult = reader.ReadToEnd();
                    reader.Close();
                    stream.Close();
                }

                owmWeather = JsonConvert.DeserializeObject<OWMReport>(jsonResult);
                //Debug.WriteLine(owmWeather.Current.WindDeg);

                // Get the weather data we actually need from the OWM weather data
                weatherReport.DateTime = UnixTimeStampToDateTime(owmWeather.Current.Dt);
                weatherReport.Latitude = owmWeather.Lat;
                weatherReport.Longitude = owmWeather.Lon;
                weatherReport.Temperature = owmWeather.Current.Temp;
                weatherReport.TemperatureUnits = "°F";
                weatherReport.Humidity = owmWeather.Current.Humidity;
                weatherReport.HumidityUnits = "%";
                weatherReport.WindSpeed = owmWeather.Current.WindSpeed;
                weatherReport.WindSpeedUnits = "MPH";
                weatherReport.WindDirection = windDirections.Aggregate((x, y) => Math.Abs(x.Item1 - owmWeather.Current.WindDeg) < Math.Abs(y.Item1 - owmWeather.Current.WindDeg) ? x : y).Item2;
                weatherReport.SkyConditions = owmWeather.Current.Weather[0].Description;
                //Debug.WriteLine($"wind speed: {owmWeather.Current.WindSpeed}");
                //Debug.WriteLine($"wind gust: {owmWeather.Current.WindGust}");

                return weatherReport;
            }
            catch (Exception ex)
            {
                // DO SOMETHING
                Debug.Write(ex);
                return null;
            }
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
