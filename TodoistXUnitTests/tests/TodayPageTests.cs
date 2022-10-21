using CommonLibs.Implementation;
using CommonLibs.Utils;
using System;
using TodoistApplication.Pages;
using Xunit;

namespace TodoistTests.tests
{
    [Collection("Todoist collection")]
    [Trait("Category", "TodayPageTests")]
    public class TodayPageTests : IDisposable
    {
        private WebDriverManager webDriverManager;
        public BaseTestFixture BaseTestFixture;
        private LoginPage loginPage;
        private TodayPage todayPage;
        private ScreenshotUtils screenshot;

        public TodayPageTests(BaseTestFixture baseTestFixture)
        {
            BaseTestFixture = baseTestFixture;
            webDriverManager = BrowserDriverFactory.GetBrowser(BaseTestFixture.Config.GetBrowserType());
            loginPage = new LoginPage(webDriverManager.Driver);
            screenshot = new ScreenshotUtils(webDriverManager.Driver);
            todayPage = new TodayPage(webDriverManager.Driver);

            webDriverManager.NavigateToURL(BaseTestFixture.Config.GetBaseUrl());
            loginPage.LoginToApplication(BaseTestFixture.Config.GetUsername(), BaseTestFixture.Config.GetPassword());
        }

        [Theory]
        [InlineData("My task title", "My task description")]
        public void VerifyANewTaskCanBeAdded(string taskTitle, string taskDescription)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a new task can be added");
            todayPage.AddNewTask(taskTitle, taskDescription);

            Assert.True(todayPage.IsTaskItemDisplayed(taskTitle), $"Task \"{taskTitle}\" was not created");
        }

        [Theory]
        [InlineData("Another task title")]
        public void VerifyATaskCanBeDeleted(string taskTitle)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a task can be deleted");
            todayPage.AddNewTask(taskTitle, "task description");

            todayPage.DeleteTask(taskTitle);

            Assert.False(todayPage.IsTaskItemDisplayed(taskTitle));
        }

        [Theory]
        [InlineData("New edited title", "New edited description")]
        public void VerifyATaskCanBeEdited(string newTitle, string newDescription)
        {
            string firstTitle = "Task title";
            string firstDescription = "Task description";
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a task can be deleted");
            todayPage.AddNewTask(firstTitle, firstDescription);

            todayPage.EditTask(firstTitle, newTitle, newDescription);

            Assert.False(todayPage.IsTaskItemDisplayed(firstTitle));
            Assert.True(todayPage.IsTaskItemDisplayed(newTitle));
        }

        [Theory]
        [InlineData("Task title", "Tomorrow")]
        public void VerifyADueDateCanBeAddedToATask(string taskTitle, string dueDate)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a due date can be added to a task");
            todayPage.AddNewTask(taskTitle, "Task description");

            todayPage.SetDueDate(taskTitle, dueDate);

            Assert.False(todayPage.IsTaskItemDisplayed(taskTitle));

            InboxPage inboxPage = todayPage.GoToInboxPage();
            Assert.True(inboxPage.IsTaskItemDisplayed(taskTitle));
        }

        [Theory]
        [InlineData("Task_1", "This is the first comment")]
        public void VerifyACommentCanBeAddedToATask(string taskTitle, string taskComment)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a new comment can be added to a task");
            todayPage.AddNewTask(taskTitle, "Task description");

            todayPage.AddCommentToATask(taskTitle, taskComment);

            Assert.True(todayPage.IsTaskCommentDisplayed(taskTitle, taskComment), "The comment was not added");
        }

        public void Dispose()
        {
            webDriverManager.CloseAllBrowser();
        }
    }
}