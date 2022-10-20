using CommonLibs.Implementation;
using System;
using TodoistApplication.Pages;
using Xunit;

namespace TodoistTests.tests
{
    [Collection("Todoist collection")]
    public class FiltersAndLabelsPageTests : IDisposable
    {
        private WebDriverManager webDriverManager;
        public BaseTestFixture BaseTestFixture;
        private LoginPage loginPage;
        private FiltersAndLabelsPage filterAndLabelPage;

        public FiltersAndLabelsPageTests(BaseTestFixture baseTestFixture)
        {
            BaseTestFixture = baseTestFixture;
            webDriverManager = BrowserDriverFactory.GetBrowser(BaseTestFixture.Config.GetBrowserType());
            loginPage = new LoginPage(webDriverManager.Driver);

            webDriverManager.NavigateToURL(BaseTestFixture.Config.GetBaseUrl());
            filterAndLabelPage = loginPage.LoginToApplication(BaseTestFixture.Config.GetUsername(), BaseTestFixture.Config.GetPassword()).GoToFilterAndLabelsPage();
        }

        [Theory]
        [InlineData("Label_1", "Blue")]
        [InlineData("Label_2", "Magenta")]
        [InlineData("Label_3", "Grape")]
        public void VerifyANewLabelCanBeAdded(string labelName, string labelColor)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a new label can be added");
            filterAndLabelPage.AddNewLabel(labelName, labelColor);

            Assert.True(filterAndLabelPage.IsLabelHeaderDisplayed(labelName), $"Label \"{labelName}\" was not created");
            filterAndLabelPage.GoToMainView();
            Assert.True(filterAndLabelPage.IsLabelItemDisplayed(labelName), $"Label \"{labelName}\" was not created");
        }

        public void Dispose()
        {
            webDriverManager.CloseAllBrowser();
        }
    }
}