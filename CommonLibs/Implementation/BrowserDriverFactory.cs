namespace CommonLibs.Implementation
{
    public class BrowserDriverFactory
    {
        private static WebDriverManager _driverManager;

        public static WebDriverManager GetBrowser(string browser)
        {
            switch (browser)
            {
                case "chrome":
                    _driverManager = new ChromeDriverManager();
                    break;

                case "edge":
                    _driverManager = new EdgeDriverManager();
                    break;

                case "firefox":
                    _driverManager = new FirefoxDriverManager();
                    break;

                default:
                    break;
            }

            return _driverManager;
        }
    }
}
