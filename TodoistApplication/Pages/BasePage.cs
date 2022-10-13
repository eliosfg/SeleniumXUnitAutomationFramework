using CommonLibs.Implementation;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;

namespace TodoistApplication.Pages
{
    public class BasePage
    {
        protected IWebDriver _driver;
        public CommonElement cmnElement;

        public BasePage()
        {
            cmnElement = new CommonElement();
        }

        protected bool isElementDisplayed(By by, int timeoutInSeconds)
        {
            try
            {
                IWebElement newWebElement = getWebDriverWait(10).Until(e => e.FindElement(by));
                return newWebElement.Size.Width > 0 && newWebElement.Size.Height > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected WebDriverWait getWebDriverWait(int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait;
        }
    }
}