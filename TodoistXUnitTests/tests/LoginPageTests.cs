using CommonLibs.Implementation;
using CommonLibs.Utils;
using System;
using TodoistApplication.Pages;
using Xunit;

namespace TodoistTests.tests
{
    [Collection("Sequence")]
    public class LoginPageTests : IClassFixture<BaseTestFixture>, IDisposable
    {
        WebDriverManager webDriverManager;
        public BaseTestFixture BaseTestFixture { get; }
        public LoginPage loginPage;
        ScreenshotUtils screenshot;

        public LoginPageTests(BaseTestFixture baseTestFixture)
        {
            BaseTestFixture = baseTestFixture;
            webDriverManager = BrowserDriverFactory.GetBrowser(BaseTestFixture.Config.GetBrowserType());
            loginPage = new LoginPage(webDriverManager.Driver);
            screenshot = new ScreenshotUtils(webDriverManager.Driver);
        }

        [Fact]
        public void VerifyLoginTest()
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify Login Test");
            webDriverManager.NavigateToURL(BaseTestFixture.Config.GetBaseUrl());
            //BaseTestFixture.ExtentReportUtils.addTestLog(Status.Info, "Performing Login");
            loginPage.LoginToApplication("eliosfg@gmail.com", "SaulFuentes1234");
            HomePage homePage = new HomePage(webDriverManager.Driver);
            
            string expectedTitle = "Today";
            string actualTitle = homePage.GetHeaderTitle();

            Assert.Contains(expectedTitle, actualTitle);
        }

        public void Dispose()
        {
            string currentExecutionTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
            string screenshotFilename = $@"{BaseTestFixture.Config.CurrentSolutionDirectory}\screenshots\test-{currentExecutionTime}.jpeg";

            /*
            Console.WriteLine(TestContext.CurrentContext.Result.Outcome);

            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                extentReportUtils.addTestLog(Status.Fail, "One or more step failed");
                screenshot.CaptureAndSaveScreenshot(screenshotFilename);
                extentReportUtils.addScreenshot(screenshotFilename);
            }*/

            webDriverManager.CloseAllBrowser();
        }
    }
}