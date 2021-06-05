using OpenQA.Selenium;
using SpillTrackerBDDTests.Drivers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpillTrackerBDDTests.Hooks
{
    [Binding]
    class Login
    {
        private const string LoginUrl = "https://localhost:5001/Identity/Account/Login";
        private const string FacilitiesUrl = "https://localhost:5001/Facilities";
        private IWebDriver _webDriver;
        private readonly string user = "tester7@example.com";
        private readonly string pass = "123456Aa*";
        private readonly string user2 = "tester3@example.com";
        private readonly string pass2 = "123456Aa*";

        private IWebElement LoginEmail => _webDriver.FindElement(By.Id("Input_Email"));
        private IWebElement LoginPass => _webDriver.FindElement(By.Id("Input_Password"));
        private IWebElement LoginSubmit => _webDriver.FindElements(By.ClassName("btn"))[0];

        [BeforeScenario(@"JoiningCompany")]
           public void BeforeScenario(Browser driver)
           {
                _webDriver = driver.Current;
                Logining();
                Thread.Sleep(5000);
           }
        public void EnsureLoginIsOpen()
        {
            if (_webDriver.Url != LoginUrl)
            {
                _webDriver.Navigate().GoToUrl(LoginUrl);
            }
        }

        public void Logining()
        {
            EnsureLoginIsOpen();
            LoginEmail.SendKeys(user);
            LoginPass.SendKeys(pass);
            LoginSubmit.Click();
        }
    }
}
    

