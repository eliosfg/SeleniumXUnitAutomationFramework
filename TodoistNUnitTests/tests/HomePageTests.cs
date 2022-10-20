using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoistApplication.Pages;

namespace TodoistNUnitTests.tests
{
    class HomePageTests : BaseTest
    {
        private LoginPage _loginPage;
        private TodayPage _homePage;

        [SetUp]
        public void SetUp()
        {
            _loginPage = new LoginPage(WebDriverManager.Driver);
            _homePage = new TodayPage(WebDriverManager.Driver);

            WebDriverManager.NavigateToURL(Config.GetBaseUrl());
            _loginPage.LoginToApplication("eliosfg@gmail.com", "SaulFuentes1234");
        }

        [TestCase("My task title", "My task description")]
        public void VerifyANewTaskCanBeAdded(string taskTitle, string taskDescription)
        {
            _extentReportUtils.createATestCase("Verify a new task can be added");
            _extentReportUtils.addTestLog(Status.Info, "Creating new task");
            _homePage.AddNewTask(taskTitle, taskDescription);

            Assert.IsTrue(_homePage.IsTaskItemDisplayed(taskTitle), $"Task \"{taskTitle}\" was not created");
        }

        [TestCase("Another task title")]
        public void VerifyATaskCanBeDeleted(string taskTitle)
        {
            _extentReportUtils.createATestCase("Verify a task can be deleted");
            _extentReportUtils.addTestLog(Status.Info, "Creating new task");
            _homePage.AddNewTask(taskTitle, "task description");

            _extentReportUtils.addTestLog(Status.Info, $"Deleting the task \"{taskTitle}\"");
            _homePage.DeleteTask(taskTitle);

            Assert.IsFalse(_homePage.IsTaskItemDisplayed(taskTitle));
        }

        [TestCase("New edited title", "New edited description")]
        public void VerifyATaskCanBeEdited(string newTitle, string newDescription)
        {
            string firstTitle = "Task title";
            string firstDescription = "Task description";
            _extentReportUtils.createATestCase("Verify a task can be deleted");
            _extentReportUtils.addTestLog(Status.Info, "Creating new task");
            _homePage.AddNewTask(firstTitle, firstDescription);

            _extentReportUtils.addTestLog(Status.Info, "Editing the task");
            _homePage.EditTask(firstTitle, newTitle, newDescription);

            Assert.IsFalse(_homePage.IsTaskItemDisplayed(firstTitle));
            Assert.IsTrue(_homePage.IsTaskItemDisplayed(newTitle));
        }

        [TestCase("Task title", "Tomorrow")]
        public void VerifyADueDateCanBeAddedToATask(string tastTitle, string dueDate)
        {
            _extentReportUtils.createATestCase("Verify a due date can be added to a task");
            _extentReportUtils.addTestLog(Status.Info, "Creating new task");
            _homePage.AddNewTask(tastTitle, "Task description");

            _extentReportUtils.addTestLog(Status.Info, $"Set due date: {dueDate}");
            _homePage.SetDueDate(tastTitle, dueDate);

            Assert.IsFalse(_homePage.IsTaskItemDisplayed(tastTitle));

            InboxPage inboxPage = _homePage.GoToInboxPage();
            Assert.True(inboxPage.IsTaskItemDisplayed(tastTitle));
        }
    }
}
