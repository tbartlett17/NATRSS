using NUnit.Framework;
using SpillTracker.Controllers;
using SpillTracker.Utilities;
namespace SpillTrackerCS.Tests.UnitTests
{
    public class  RegexDensityParse
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {   //arrange
            string input = "1.51 at 68 °F (NTP, 1992)";
            //act
            double output = RegexParserUtilities.RegexDensityParse(input);
            //assert
            Assert.AreEqual(1.51, output);
        }
    }
}