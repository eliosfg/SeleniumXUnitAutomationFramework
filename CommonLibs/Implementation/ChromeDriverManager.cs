using OpenQA.Selenium.Chrome;

namespace CommonLibs.Implementation
{
    public class ChromeDriverManager : WebDriverManager
    {
        protected override void createWebDriver()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--lang=en");
            chromeOptions.AddArguments("start-maximized");

            Driver = new ChromeDriver(chromeOptions);

            PageLoadTimeout = 50;
            ElementDetectionTimeout = 10;
        }
    }
}
