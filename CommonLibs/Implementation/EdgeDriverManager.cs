using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace CommonLibs.Implementation
{
    internal class EdgeDriverManager : WebDriverManager
    {
        protected override void createWebDriver()
        {
            Driver = new EdgeDriver();
            Driver.Manage().Window.Maximize();

            PageLoadTimeout = 50;
            ElementDetectionTimeout = 10;
        }
    }
}