using CommonLibs.Implementation;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using SeleniumExtras.WaitHelpers;

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

        protected bool IsElementDisplayed(By by, int timeoutInSeconds)
        {
            try
            {
                IWebElement newWebElement = GetWebDriverWait(10).Until(e => e.FindElement(by));
                return newWebElement.Size.Width > 0 && newWebElement.Size.Height > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected WebDriverWait GetWebDriverWait(int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait;
        }

        protected IWebElement WaitAndFindElement(By locator)
        {
            return GetWebDriverWait(10).Until(ExpectedConditions.ElementIsVisible(locator));
        }
    }
}