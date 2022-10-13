using OpenQA.Selenium;
using System;

namespace CommonLibs.Implementation
{
    public abstract class WebDriverManager
    {
        private IWebDriver _driver;
        private int _pageLoadTimeout;
        private int _elementDetectionTimeout;
        public int PageLoadTimeout { private get { return _pageLoadTimeout; } set { _pageLoadTimeout = value; } }
        public int ElementDetectionTimeout { private get { return _elementDetectionTimeout; } set { _elementDetectionTimeout = value; } }


        public IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    createWebDriver();
                }
                return _driver;
            }

            set { _driver = value; }
        }

        protected abstract void createWebDriver();

        public void NavigateToURL(string url)
        {
            url = url.Trim();

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(PageLoadTimeout);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_elementDetectionTimeout);

            Driver.Navigate().GoToUrl(url);

            Driver.Url = url;
        }

        public void CloseBrowser()
        {
            if (Driver != null)
            {
                Driver.Close();
            }
        }

        public void CloseAllBrowser()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }

        public string GetPageTitle()
        {
            return Driver.Title;
        }
    }
}
