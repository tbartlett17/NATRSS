using OpenQA.Selenium;
using SpillTrackerBDDTests.Drivers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Configuration;


namespace SpillTrackerBDDTests.Hooks
{
    class ViewFormIndexPage
    {
        IConfiguration Configuration { get; set; }

        private IWebDriver _webDriver;
        private IWebElement LoginEmail => _webDriver.FindElement(By.Id("Input_Email"));
        private IWebElement LoginPass => _webDriver.FindElement(By.Id("Input_Password"));
        private IWebElement LoginSubmit => _webDriver.FindElements(By.ClassName("btn"))[0];


        private const string LoginUrl = "https://localhost:5001/Identity/Account/Login";
        
        private readonly string userWithNoCompany = "jackio@example.com";
        private readonly string userWithCompany = "clark@example.com";

        



        [BeforeScenario(@"ViewFormIndexPage")]
        public void BeforeScenario(Browser driver)
        {
            //setup the user secrets
            var builder = new ConfigurationBuilder().AddUserSecrets<ViewFormIndexPage>();
            Configuration = builder.Build();

            _webDriver = driver.Current;
            Login(userWithCompany, Configuration["SeedUserPW"]);
            Thread.Sleep(5000);
        }
        public void EnsureLoginIsOpen()
        {
            if (_webDriver.Url != LoginUrl)
            {
                _webDriver.Navigate().GoToUrl(LoginUrl);
            }
        }

        public void Login(string username, string password)
        {
            EnsureLoginIsOpen();
            LoginEmail.SendKeys(username);
            LoginPass.SendKeys(password);
            LoginSubmit.Click();
        }
    }
}
