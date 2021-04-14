using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpillTrackerCS.Tests.UnitTests
{
    class BadEmailInput
    {
        [SetUp]
        public void SetUp() 
        {
        }

        [Test]
        public void BadEmailInputRegister_test() 
        {
            //arrange
            string input = "@gm@.com";
            //act
            string output = (model).name(input);
            //assert
            Assert.AreSame("a@g.com", output);

        } 

    }
}
