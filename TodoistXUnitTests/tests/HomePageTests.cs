using CommonLibs.Implementation;
using CommonLibs.Utils;
using System;
using TodoistApplication.Pages;
using Xunit;

namespace TodoistTests.tests
{
    [Collection("Sequence")]
    public class HomePageTests : IClassFixture<BaseTestFixture>, IDisposable
    {
        private WebDriverManager webDriverManager;
        private BaseTestFixture BaseTestFixture { get; }
        private LoginPage loginPage;
        private HomePage homePage;
        private ScreenshotUtils screenshot;

        public HomePageTests(BaseTestFixture baseTestFixture)
        {
            BaseTestFixture = baseTestFixture;
            webDriverManager = BrowserDriverFactory.GetBrowser(BaseTestFixture.Config.GetBrowserType());
            loginPage = new LoginPage(webDriverManager.Driver);
            screenshot = new ScreenshotUtils(webDriverManager.Driver);
            homePage = new HomePage(webDriverManager.Driver);

            webDriverManager.NavigateToURL(BaseTestFixture.Config.GetBaseUrl());
            loginPage.LoginToApplication("eliosfg@gmail.com", "SaulFuentes1234");
        }

        [Theory]
        [InlineData("My task title", "My task description")]
        public void VerifyANewTaskCanBeAdded(string taskTitle, string taskDescription)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a new task can be added");
            //BaseTestFixture.ExtentReportUtils.addTestLog(Status.Info, "Creating new task");
            homePage.AddNewTask(taskTitle, taskDescription);

            Assert.True(homePage.IsTaskItemDisplayed(taskTitle), $"Task \"{taskTitle}\" was not created");
        }

        [Theory]
        [InlineData("Another task title")]
        public void VerifyATaskCanBeDeleted(string taskTitle)
        {
            BaseTestFixture.ExtentReportUtils.createATestCase("Verify a task can be deleted");
            //BaseTestFixture.ExtentReportUtils.addTestLog(Status.Info, "Creating new task");
            homePage.AddNewTask(taskTitle, "task description");

            //BaseTestFixture.ExtentReportUtils.addTestLog(Status.Info, $"Deleting the task \"{taskTitle}\"");
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
            //BaseTestFixture.ExtentReportUtils.addTestLog(Status.Info, "Creating new task");
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
            //BaseTestFixture.ExtentReportUtils.addTestLog(Status.Info, "Creating new task");
            homePage.AddNewTask(tastTitle, "Task description");

            //BaseTestFixture.ExtentReportUtils.addTestLog(Status.Info, $"Set due date: {dueDate}");
            homePage.SetDueDate(tastTitle, dueDate);

            Assert.False(homePage.IsTaskItemDisplayed(tastTitle));

            InboxPage inboxPage = homePage.GoToInboxPage();
            Assert.True(inboxPage.IsTaskItemDisplayed(tastTitle));
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