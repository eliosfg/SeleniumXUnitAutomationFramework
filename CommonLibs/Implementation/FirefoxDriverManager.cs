using OpenQA.Selenium.Firefox;

namespace CommonLibs.Implementation
{
    public class FirefoxDriverManager : WebDriverManager
    {
        protected override void createWebDriver()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArguments("--lang=en");

            Driver = new FirefoxDriver(options);
            Driver.Manage().Window.Maximize();

            PageLoadTimeout = 50;
            ElementDetectionTimeout = 10;
        }
    }
}
