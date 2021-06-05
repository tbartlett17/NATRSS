using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace SpillTrackerBDDTests.Steps
{

    [Binding]
    public class JoiningACompanySteps
    {
        private readonly ScenarioContext _ctx;
        private string _hostBaseName = @"https://localhost:5001";
        private readonly IWebDriver _driver;

        public class Companies
        {
            public string CName { get; set; }
            public string CAccessCode { get; set; }
        }

        public JoiningACompanySteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _ctx = scenarioContext;
            _driver = driver;
        }

        [Given(@"the following users exist")]
        public void GivenTheFollowingUsersExist(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the following Companies exist")]
        public void GivenTheFollowingCompaniesExist(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the user is an employee")]
        public void GivenTheUserIsAnEmployee()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"they are not connected with a company")]
        public void GivenTheyAreNotConnectedWithACompany()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the user is an facility manager")]
        public void GivenTheUserIsAnFacilityManager()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"they are connected with a company")]
        public void GivenTheyAreConnectedWithACompany()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the user goes to the facility tab")]
        public void WhenTheUserGoesToTheFacilityTab()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"enters a company access code")]
        public void WhenEntersACompanyAccessCode()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the user goes to the facility tab they should not be prompted to join a company")]
        public void WhenTheUserGoesToTheFacilityTabTheyShouldNotBePromptedToJoinACompany()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"they should be connected to that specified company")]
        public void ThenTheyShouldBeConnectedToThatSpecifiedCompany()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"they should be able to create new facilities")]
        public void ThenTheyShouldBeAbleToCreateNewFacilities()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
