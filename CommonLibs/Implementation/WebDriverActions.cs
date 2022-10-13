using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibs.Implementation
{
    public class WebDriverActions
    {
        public static void moveToElement(IWebElement webElement, IWebDriver webDriver)
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webElement).Perform();
        }
    }
}
