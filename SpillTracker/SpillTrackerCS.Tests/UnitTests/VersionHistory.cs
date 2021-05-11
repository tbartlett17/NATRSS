using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SpillTracker.Utilities;
using SpillTracker.Models;
using Microsoft.Extensions.Configuration;

namespace SpillTrackerCS.Tests.UnitTests
{
    class VersionHistory
    {
        IConfiguration Configuration { get; set; }


        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<VersionHistory>();

            Configuration = builder.Build();
        }

        [Test]
        public void VerifyDateIsNotNull()
        {
            //arrange
            
            var apiKey = Configuration["NatrGitkey"];

            //act
           
            //assert
           
        }


    }
}
