using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpillTrackerBDDTests.Drivers
{
    public class Browser : IDisposable
    {

        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private bool _isDisposed;

        public Browser()
        {
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        public IWebDriver Current => _currentWebDriverLazy.Value;

        private IWebDriver CreateWebDriver()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AcceptInsecureCertificates = true;

            var chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);

            return chromeDriver;
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;
            if (_currentWebDriverLazy.IsValueCreated)
                Current.Quit();
            _isDisposed = true;
        }
    }


}

