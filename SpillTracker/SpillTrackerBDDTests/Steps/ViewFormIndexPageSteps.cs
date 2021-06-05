using System;
using TechTalk.SpecFlow;

namespace SpillTrackerBDDTests.Steps
{
    [Binding]
    public class ViewFormIndexPageSteps
    {
        [Given(@"I am signed in as the seeded user jackio@example\.com who has not yet joined a company")]
        public void GivenIAmSignedInAsTheSeededUserJackioExample_ComWhoHasNotYetJoinedACompany()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I am signed in as the seeded user clark@example\.com")]
        public void GivenIAmSignedInAsTheSeededUserClarkExample_Com()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I navigate to the Forms Index page")]
        public void WhenINavigateToTheFormsIndexPage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I will see a message saying that ""(.*)""My Facilities""(.*)""")]
        public void ThenIWillSeeAMessageSayingThatMyFacilities(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I will see a table containing my company's spill forms")]
        public void ThenIWillSeeATableContainingMyCompanySSpillForms()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
