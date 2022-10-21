using CommonLibs.Implementation;
using CommonLibs.Utils;
using System;
using TodoistApplication.Pages;
using Xunit;

namespace TodoistTests.tests
{
    [Collection("Todoist collection")]
    [Trait("Category", "LoginTests")]
    public class LoginPageTests : IDisposable
    {
        WebDriverManager webDriverManager;
        public BaseTestFixture BaseTestFixture;
        public LoginPage loginPage;
        ScreenshotUtils screenshot;

        public LoginPageTests(BaseTestFixture baseTestFixture)
        {
            this.BaseTestFixture = baseTestFixture;
            webDriverManager = BrowserDriverFactory.GetBrowser(BaseTestFixture.Config.GetBrowserType());
            loginPage = new LoginPage(webDriverManager.Driver);
            screenshot = new ScreenshotUtils(webDriverManager.Driver);
        }

        [Fact]
        public void VerifyLoginTest()
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify Login Test");
            webDriverManager.NavigateToURL(BaseTestFixture.Config.GetBaseUrl());

            loginPage.LoginToApplication(BaseTestFixture.Config.GetUsername(), BaseTestFixture.Config.GetPassword());
            TodayPage homePage = new TodayPage(webDriverManager.Driver);
            
            string expectedTitle = "Today";
            string actualTitle = homePage.GetHeaderTitle();

            Assert.Contains(expectedTitle, actualTitle);
        }

        public void Dispose()
        {
            webDriverManager.CloseAllBrowser();
        }
    }
}