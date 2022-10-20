using CommonLibs.Implementation;
using System;
using TodoistApplication.Pages;
using Xunit;

namespace TodoistTests.tests
{
    [Collection("Todoist collection")]
    public class InboxPageTests : IDisposable
    {
        private WebDriverManager webDriverManager;
        public BaseTestFixture BaseTestFixture;
        private LoginPage loginPage;
        private InboxPage inboxPage;

        public InboxPageTests(BaseTestFixture baseTestFixture)
        {
            BaseTestFixture = baseTestFixture;
            webDriverManager = BrowserDriverFactory.GetBrowser(BaseTestFixture.Config.GetBrowserType());
            loginPage = new LoginPage(webDriverManager.Driver);

            webDriverManager.NavigateToURL(BaseTestFixture.Config.GetBaseUrl());
            inboxPage = loginPage.LoginToApplication(BaseTestFixture.Config.GetUsername(), BaseTestFixture.Config.GetPassword()).GoToInboxPage();
        }

        [Theory]
        [InlineData("Priority 1 task", "My task description", "Priority 1")]
        [InlineData("Priority 2 task", "My task description", "Priority 2")]
        [InlineData("Priority 4 task", "My task description", "Priority 4")]
        public void VerifyANewPriorityTaskCanBeAdded(string taskTitle, string taskDescription, string priority)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a new Priority Task can be added");
            inboxPage.AddNewPriorityTask(taskTitle, taskDescription, priority);

            Assert.True(inboxPage.IsTaskItemDisplayed(taskTitle), $"Task \"{taskTitle}\" was not created");
        }

        [Theory]
        [InlineData("Task A", "4")]
        [InlineData("Task B", "2")]
        public void VerifyATaskPriorityCanBeChanged(string taskTitle, string newPriority)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify that a task's priority can be changed");
            inboxPage.AddNewPriorityTask(taskTitle, "description", "Priority 1");

            inboxPage.ChangeTaskPriority(taskTitle, newPriority);

            string actualResult = inboxPage.GetTaskPriority(taskTitle);
            string expectedResult = "P" + newPriority;

            Assert.Equal(expectedResult, actualResult);
        }

        public void Dispose()
        {
            webDriverManager.CloseAllBrowser();
        }
    }
}