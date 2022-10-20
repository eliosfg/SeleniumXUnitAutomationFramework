using CommonLibs.Implementation;
using CommonLibs.Utils;
using System;
using TodoistApplication.Pages;
using Xunit;

namespace TodoistTests.tests
{
    [Collection("Todoist collection")]
    public class TodayPageTests : IDisposable
    {
        private WebDriverManager webDriverManager;
        public BaseTestFixture BaseTestFixture;
        private LoginPage loginPage;
        private TodayPage homePage;
        private ScreenshotUtils screenshot;

        public TodayPageTests(BaseTestFixture baseTestFixture)
        {
            BaseTestFixture = baseTestFixture;
            webDriverManager = BrowserDriverFactory.GetBrowser(BaseTestFixture.Config.GetBrowserType());
            loginPage = new LoginPage(webDriverManager.Driver);
            screenshot = new ScreenshotUtils(webDriverManager.Driver);
            homePage = new TodayPage(webDriverManager.Driver);

            webDriverManager.NavigateToURL(BaseTestFixture.Config.GetBaseUrl());
            loginPage.LoginToApplication(BaseTestFixture.Config.GetUsername(), BaseTestFixture.Config.GetPassword());
        }

        [Theory]
        [InlineData("My task title", "My task description")]
        public void VerifyANewTaskCanBeAdded(string taskTitle, string taskDescription)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a new task can be added");
            homePage.AddNewTask(taskTitle, taskDescription);

            Assert.True(homePage.IsTaskItemDisplayed(taskTitle), $"Task \"{taskTitle}\" was not created");
        }

        [Theory]
        [InlineData("Another task title")]
        public void VerifyATaskCanBeDeleted(string taskTitle)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a task can be deleted");
            homePage.AddNewTask(taskTitle, "task description");

            homePage.DeleteTask(taskTitle);

            Assert.False(homePage.IsTaskItemDisplayed(taskTitle));
        }

        [Theory]
        [InlineData("New edited title", "New edited description")]
        public void VerifyATaskCanBeEdited(string newTitle, string newDescription)
        {
            string firstTitle = "Task title";
            string firstDescription = "Task description";
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a task can be deleted");
            homePage.AddNewTask(firstTitle, firstDescription);

            homePage.EditTask(firstTitle, newTitle, newDescription);

            Assert.False(homePage.IsTaskItemDisplayed(firstTitle));
            Assert.True(homePage.IsTaskItemDisplayed(newTitle));
        }

        [Theory]
        [InlineData("Task title", "Tomorrow")]
        public void VerifyADueDateCanBeAddedToATask(string tastTitle, string dueDate)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a due date can be added to a task");
            homePage.AddNewTask(tastTitle, "Task description");

            homePage.SetDueDate(tastTitle, dueDate);

            Assert.False(homePage.IsTaskItemDisplayed(tastTitle));

            InboxPage inboxPage = homePage.GoToInboxPage();
            Assert.True(inboxPage.IsTaskItemDisplayed(tastTitle));
        }

        public void Dispose()
        {
            webDriverManager.CloseAllBrowser();
        }
    }
}