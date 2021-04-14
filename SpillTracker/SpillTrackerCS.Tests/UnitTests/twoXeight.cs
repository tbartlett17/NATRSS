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
    }
}
