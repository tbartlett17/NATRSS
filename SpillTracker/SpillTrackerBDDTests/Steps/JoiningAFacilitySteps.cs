using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace SpillTrackerBDDTests.Steps
{
    



    [Binding]
    public class JoiningAFacilitySteps
    {

        private readonly ScenarioContext _ctx;
        private string _hostBaseName = @"https://localhost:5001";
        private readonly IWebDriver _driver;

        public class Facilities
        {
            public string FName { get; set; }
            public string FAccessCode { get; set; }
        }

        public JoiningAFacilitySteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _ctx = scenarioContext;
            _driver = driver;
        }


        [Given(@"the following users exist")]
        public void GivenTheFollowingUsersExist(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the following facilities exist")]
        public void GivenTheFollowingFacilitiesExist(Table table)
        {
           
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the user is an employee")]
        public void GivenTheUserIsAnEmployee()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"they are connected with a company")]
        public void GivenTheyAreConnectedWithACompany()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the user is an facility manager")]
        public void GivenTheUserIsAnFacilityManager()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the user goes to the facility tab")]
        public void WhenTheUserGoesToTheFacilityTab()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"enters a facility access code correctly")]
        public void WhenEntersAFacilityAccessCodeCorrectly()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"they should be able to view that facility")]
        public void ThenTheyShouldBeAbleToViewThatFacility()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
