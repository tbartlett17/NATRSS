// Tests Written by Tyler Bartlett for user story #177279012

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SpillTracker.Utilities;
using SpillTracker.Models;
using Microsoft.Extensions.Configuration;

namespace SpillTrackerCS.Tests.UnitTests
{
    class WeatherAPI
    {
        IConfiguration Configuration { get; set; }
       

        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<WeatherAPI>();

            Configuration = builder.Build();
        }


        [Test]
        public void VerifyCurrentTimeMatchesCurrentTimeRetunedByApi()
        {   
            //arrange
            string coords = "44.853736, -123.240071";
            DateTime currentTime = DateTime.Now;
            DateTime currentTimeUTC = currentTime.ToUniversalTime();
            var apiKey = Configuration["OpenWeatherMapsAPIKey"];

            //act
            WeatherReport wr = GetWeatherInfo.GetWeather(coords, currentTimeUTC, apiKey);
            //Console.WriteLine(wr.DateTime);
            
            //assert
            Assert.AreEqual(currentTime.ToString("yyyy-MM-dd hh:mm tt"), wr.DateTime.ToString("yyyy-MM-dd hh:mm tt"));
        }

        [Test]
        public void VerifyTime29MinutesAgoGetsCurrentWeather()
        {
            //arrange
            string coords = "44.853736, -123.240071";
            DateTime currentTime = DateTime.Now;
            DateTime currentTimeUTC = currentTime.AddMinutes(-29).ToUniversalTime();
            var apiKey = Configuration["OpenWeatherMapsAPIKey"];

            //act
            WeatherReport wr = GetWeatherInfo.GetWeather(coords, currentTimeUTC, apiKey);
            //Console.WriteLine(wr.DateTime);

            //assert
            Assert.AreEqual(currentTime.ToString("yyyy-MM-dd hh:mm tt"), wr.DateTime.ToString("yyyy-MM-dd hh:mm tt"));
        }

        [Test]
        public void VerifyTime30MinAgoMatchesTimeRetunedByApi()
        {
            //arrange
            string coords = "44.853736, -123.240071";
            DateTime Time31MinAgo = DateTime.Now.AddMinutes(-30);
            DateTime Time31MinAgoUTC = Time31MinAgo.ToUniversalTime();
            var apiKey = Configuration["OpenWeatherMapsAPIKey"];

            //act
            WeatherReport wr = GetWeatherInfo.GetWeather(coords, Time31MinAgoUTC, apiKey);
            //Console.WriteLine(wr.DateTime);

            //assert
            Assert.AreEqual(Time31MinAgo.ToString("yyyy-MM-dd hh:mm tt"), wr.DateTime.ToString("yyyy-MM-dd hh:mm tt"));
        }

        [Test]
        public void VerifyTime17Hrsand30MinAgoMatchesTimeRetunedByApi()
        {
            //arrange
            string coords = "44.853736, -123.240071";
            DateTime Time17Hrs30MinAgo = DateTime.Now.AddHours(-17).AddMinutes(-30);
            DateTime Time17Hrs30MinAgoUTC = Time17Hrs30MinAgo.ToUniversalTime();
            var apiKey = Configuration["OpenWeatherMapsAPIKey"];

            //act
            WeatherReport wr = GetWeatherInfo.GetWeather(coords, Time17Hrs30MinAgoUTC, apiKey);
            //Console.WriteLine(wr.DateTime);

            //assert
            Assert.AreEqual(Time17Hrs30MinAgo.ToString("yyyy-MM-dd hh:mm tt"), wr.DateTime.ToString("yyyy-MM-dd hh:mm tt"));
        }

        [Test]
        public void VerifyTime4Days23Hrs59MinAgoMatchesTimeRetunedByApi()
        {
            //arrange
            string coords = "44.853736, -123.240071";
            DateTime Time4Days23Hrs59MinAgo = DateTime.Now.AddDays(-4).AddHours(-23).AddMinutes(-59);
            DateTime Time4Days23Hrs59MinAgoUTC = Time4Days23Hrs59MinAgo.ToUniversalTime();
            var apiKey = Configuration["OpenWeatherMapsAPIKey"];

            //act
            WeatherReport wr = GetWeatherInfo.GetWeather(coords, Time4Days23Hrs59MinAgoUTC, apiKey);
            //Console.WriteLine(wr.DateTime);

            //assert
            Assert.AreEqual(Time4Days23Hrs59MinAgo.ToString("yyyy-MM-dd hh:mm tt"), wr.DateTime.ToString("yyyy-MM-dd hh:mm tt"));
        }

        [Test]
        public void VerifyTime5Days1MinAgoMatchesTimeRetunedByApi()
        {
            //arrange
            string coords = "44.853736, -123.240071";
            DateTime Time5Days1MinAgo = DateTime.Now.AddDays(-5).AddMinutes(-1);
            DateTime Time5Days1MinAgoUTC = Time5Days1MinAgo.ToUniversalTime();
            var apiKey = Configuration["OpenWeatherMapsAPIKey"];

            //act
            WeatherReport wr = GetWeatherInfo.GetWeather(coords, Time5Days1MinAgoUTC, apiKey);
            //Console.WriteLine(wr.DateTime);

            //assert
            Assert.IsNull(wr);
        }

        [Test]
        public void VerifyNullReturnForDate6DaysAgo()
        {
            //arrange
            string coords = "44.853736, -123.240071";
            DateTime Time6DaysAgo = DateTime.Now.AddDays(-6);
            var apiKey = Configuration["OpenWeatherMapsAPIKey"];

            //act
            WeatherReport wr = GetWeatherInfo.GetWeather(coords, Time6DaysAgo, apiKey);
            //Console.WriteLine(wr.DateTime);

            //assert
            Assert.IsNull(wr);
        }

        [Test]
        public void VerifyNullReturnForDate1HrInFuture()
        {
            //arrange
            string coords = "44.853736, -123.240071";
            DateTime Time1HrInFuture = DateTime.Now.AddHours(1);
            var apiKey = Configuration["OpenWeatherMapsAPIKey"];

            //act
            WeatherReport wr = GetWeatherInfo.GetWeather(coords, Time1HrInFuture, apiKey);
            //Console.WriteLine(wr.DateTime);

            //assert
            Assert.IsNull(wr);
        }
    }
}
