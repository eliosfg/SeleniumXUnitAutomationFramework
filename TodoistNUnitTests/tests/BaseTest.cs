using CommonLibs.Utils;
using CommonLibs.Implementation;
using NUnit.Framework;
using TodoistNUnitTests.config;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports;
using System;

namespace TodoistNUnitTests.tests
{
    public class BaseTest
    {
        public Config Config { get; private set; }
        public WebDriverManager WebDriverManager { get; private set; }
        protected ExtentReportUtils _extentReportUtils;
        public ScreenshotUtils Screenshot { get { return _screenshot; } }
        private ScreenshotUtils _screenshot;

        [OneTimeSetUp]
        public void PreSetup()
        {
            Config = new Config();
            _extentReportUtils = new ExtentReportUtils(Config.GetReportFilename());
        }

        [SetUp]
        public void Setup()
        {
            WebDriverManager = BrowserDriverFactory.GetBrowser(Config.GetBrowserType());
            _screenshot = new ScreenshotUtils(WebDriverManager.Driver);
        }

        [TearDown]
        public void TearDown()
        {
            string currentExecutionTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
            string screenshotFilename = $@"{Config.CurrentProjectDirectory}\screenshots\test-{currentExecutionTime}.jpeg";

            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                _extentReportUtils.addTestLog(Status.Fail, "One or more step failed");
                _screenshot.CaptureAndSaveScreenshot(screenshotFilename);
                _extentReportUtils.addScreenshot(screenshotFilename);
            }

            WebDriverManager.CloseAllBrowser();
        }

        [OneTimeTearDown]
        public void PostCleanUp()
        {
            _extentReportUtils.flushReport();
        }
    }
}
