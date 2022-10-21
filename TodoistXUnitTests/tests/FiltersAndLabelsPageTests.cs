using CommonLibs.Implementation;
using System;
using TodoistApplication.Pages;
using Xunit;

namespace TodoistTests.tests
{
    [Collection("Todoist collection")]
    [Trait("Category", "LabelandFilterTests")]
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

            Assert.True(filterAndLabelPage.IsHeaderTitleDisplayed(labelName), $"Label \"{labelName}\" was not created");
            filterAndLabelPage.GoToMainView();
            Assert.True(filterAndLabelPage.IsLabelItemDisplayed(labelName), $"Label \"{labelName}\" was not created");
        }

        [Theory]
        [InlineData("Label_1")]
        public void VerifyALabelCanBeDeleted(string labelName)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a new label can be deleted");
            filterAndLabelPage.AddNewLabel(labelName, "Blue");
            filterAndLabelPage.GoToMainView();

            filterAndLabelPage.DeleteLabel(labelName);

            Assert.False(filterAndLabelPage.IsLabelItemDisplayed(labelName), $"The label {labelName} was not deleted");
        }

        [Theory]
        [InlineData("Filter_1", "Filter_Query_1", "Blue")]
        [InlineData("Filter_2", "Filter_Query_2", "Magenta")]
        public void VerifyANewFilterCanBeAdded(string filterName, string filterQuery, string filterColor)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a new filter can be added");
            filterAndLabelPage.AddNewFilter(filterName, filterQuery, filterColor);

            Assert.True(filterAndLabelPage.IsHeaderTitleDisplayed(filterName), $"Filter \"{filterName}\" was not created");
            filterAndLabelPage.GoToMainView();
            Assert.True(filterAndLabelPage.IsFilterItemDisplayed(filterName), $"Label \"{filterName}\" was not created");
        }

        [Theory]
        [InlineData("Filter_1")]
        public void VerifyAFilterCanBeDeleted(string filterName)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a filter can be deleted");
            filterAndLabelPage.AddNewFilter(filterName, "Filter_query", "Blue");

            filterAndLabelPage.GoToMainView();
            filterAndLabelPage.DeleteFilter(filterName);

            Assert.False(filterAndLabelPage.IsFilterItemDisplayed(filterName), $"The filter {filterName} was not deleted");
        }

        [Theory]
        [InlineData("LabelName", "NewLabelName", "Light Blue")]
        public void VerifyALabelCanBeEdited(string labelName, string newLabelName, string newLabelColor)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a label can be edited");
            filterAndLabelPage.AddNewLabel(labelName, "Blue");

            filterAndLabelPage.GoToMainView();
            filterAndLabelPage.EditLabel(labelName, newLabelName, newLabelColor);

            Assert.False(filterAndLabelPage.IsLabelItemDisplayed(labelName), "The label was not edited");
            Assert.True(filterAndLabelPage.IsLabelItemDisplayed(newLabelName));
            Assert.Equal(newLabelColor, filterAndLabelPage.GetLabelColor(newLabelName));
        }

        public void Dispose()
        {
            webDriverManager.CloseAllBrowser();
        }
    }
}