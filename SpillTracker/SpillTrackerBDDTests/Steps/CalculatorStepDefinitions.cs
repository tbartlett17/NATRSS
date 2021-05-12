using NUnit.Framework;
using SpillTrackerBDDTests.src;
using TechTalk.SpecFlow;

namespace SpillTrackerBDDTests.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly Calculator _calculator = new Calculator();
        private int _result;

        public CalculatorStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 

            _calculator.FirstNumber = number;
           // _scenarioContext["leftop"] = number;

            //_scenarioContext.Pending();
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 

            _calculator.SecondNumber = number;
            //_scenarioContext["rightop"] = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            //TODO: implement act (action) logic

            _result = _calculator.Add();
           // _scenarioContext["result"] = _calculator.Add((int)_scenarioContext["leftOp"], (int)_scenarioContext["rightOp"]);
        }

        [When("the two numbers are divided")]
        public void WhenTheTwoNumbersAreDivided()
        {
            _result = _calculator.Divide();
            //_scenarioContext["result"] = _calculator.Divide((int)_scenarioContext["leftOp"], (int)_scenarioContext["rightOp"]);
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            //TODO: implement assert (verification) logic

            Assert.That(_result, Is.EqualTo(result));
            //Assert.That(_scenarioContext["result"], Is.EqualTo(result));
        }
    }
}
