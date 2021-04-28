using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpillTracker.Models;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace SpillTrackerCS.Tests.Utilities
{
    public static class MockTest 
    {
        public static Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(() => entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(() => entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(() => entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => entities.GetEnumerator());
            return mockSet;
        }

        public static List<Chemical> GenerateChemsList() 
        {
            var chems = new List<Chemical>();
            chems.Add(new Chemical{
                Name = "Acid",
                Id = 1
            });
            chems.Add(new Chemical{
                Name = "Argon",
                Id = 2
            });
            chems.Add(new Chemical{
                Name = "Bocho",
                Id = 3
            });
            chems.Add(new Chemical{
                Name = "Cadmium",
                Id = 4
            });
            chems.Add(new Chemical{
                Name = "Delta6",
                Id = 5
            });
            chems.Add(new Chemical{
                Name = "2-3Sodium",
                Id = 6
            });
            return chems;
        }
    }
}