using AventStack.ExtentReports;
using NUnit.Framework;
using TodoistApplication.Pages;

namespace TodoistNUnitTests.tests
{
    [TestFixture]
    public class LoginPageTests : BaseTest
    {
        private LoginPage _loginPage;

        [SetUp]
        public void SetUp()
        {
            _loginPage = new LoginPage(WebDriverManager.Driver);
            WebDriverManager.NavigateToURL(Config.GetBaseUrl());
        }

        [Test]
        public void VerifyLoginTest()
        {
            _extentReportUtils.createATestCase("Verify Login Test");
            _extentReportUtils.addTestLog(Status.Info, "Performing Login");
            _loginPage.LoginToApplication("eliosfg@gmail.com", "SaulFuentes1234");
            TodayPage homePage = new TodayPage(WebDriverManager.Driver);

            string expectedTitle = "Today";
            string actualTitle = homePage.GetHeaderTitle();

            Assert.IsTrue(actualTitle.Contains(expectedTitle));
        }
    }
}