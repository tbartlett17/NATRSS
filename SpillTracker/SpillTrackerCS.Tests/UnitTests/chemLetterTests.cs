using NUnit.Framework;
using SpillTracker.Controllers;
using SpillTracker.Utilities;
using SpillTracker.Models.Repositories;
using SpillTracker.Models.Interfaces;
using Moq;
using SpillTracker.Models;
using Microsoft.EntityFrameworkCore;
using SpillTrackerCS.Tests.Utilities;
using System.Linq;

namespace SpillTrackerCS.Tests.UnitTests
{
    public class ByFirstLetterTests
    {
        private ISpillTrackerChemicalRepository chemicalRepo;

        private Mock<SpillTrackerDbContext> _MockContext;

        private Mock<DbSet<Chemical>> _dbSet;

        [SetUp]
        public void Setup()
        {
            Mock chem = new Mock<ISpillTrackerChemicalRepository>();
            var data = MockTest.GenerateChemsList();
            _dbSet = MockTest.GetMockDbSet<Chemical>(data.AsQueryable());
            _MockContext = new Mock<SpillTrackerDbContext>();
            _MockContext.Setup(ctx => ctx.Set<Chemical>()).Returns(_dbSet.Object);
            chemicalRepo = new SpillTrackerChemicalRepository(_MockContext.Object);
        }

        [Test]
        public void ChemicalList_AssertLetterGetsChemicalsList()
        {
            //Arrange

            //Act
                //List temp = new List();
                var temp = chemicalRepo.ByFirstLetter("A");
            //Assert
                Assert.AreEqual(temp.Count(), 2);
        }

        [Test]
        public void ChemicalList_AssertLetter_B_Gets_Bocho()
        {
           //Arrange

            //Act
                //List temp = new List();
                var temp = chemicalRepo.ByFirstLetter("B");
                
            //Assert
                Assert.That(temp.First().Name, Is.EqualTo("Bocho"));
                //Assert.AreEqual(temp.Count(), 1); 
        }

         [Test]
        public void ChemicalList_AssertLetterReturnsNothing()
        {
            //Arrange

            //Act
                //List temp = new List();
                var temp = chemicalRepo.ByFirstLetter("Z");
            //Assert
                Assert.AreEqual(temp.Count(), 0);
        }

        [Test]
         public void ChemicalList_AssertLetter_number_Gets_23Sodium()
        {
           //Arrange

            //Act
                //List temp = new List();
                var temp = chemicalRepo.ByFirstLetter("#");
                
            //Assert 
                Assert.That(temp.First().Name, Is.EqualTo("2-3Sodium"));
        }

        [Test]
        public void ChemicalList_AssertNullReturnsAll()
        {
            //Arrange

            //Act
                //List temp = new List();
                var temp = chemicalRepo.ByFirstLetter("");
            //Assert
                Assert.AreEqual(temp.Count(), 6);
        }
    }
}