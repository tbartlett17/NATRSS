using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpillTrackerCS.Tests.UnitTests
{
    class twoXeight
    {
        [SetUp]
        public void SetuUp() 
        {

        }
        [Test]
        public void mathLogic_test() 
        {
            //arrange
            int x = 8; int y;
            //act
            y = x * x;
            //assert
            Assert.AreEqual(64,y);

        } 

        //Tyler's Test
        [Test]
        public void AnotherTest()
        {
            //arrange
            double x = 125.66;
            int y = 5;
            double result;

            //act
            result = Math.Pow(x, 2) * y;
            //assert
            Assert.IsNotNull(x);
            Assert.IsNotNull(y);
            Assert.IsNotNull(result);
            Assert.AreEqual(78952.178, result);

        }
    }
}
