﻿using CommonLibs.Implementation;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace TodoistApplication.Pages
{
    public class TodayPage: BasePage
    {
        private IWebElement headerTitle => _driver.FindElement(By.CssSelector("header h1"));
        private IWebElement addTaskLnkButton => _driver.FindElement(By.CssSelector("button.plus_add_button"));
        private IWebElement taskTitleInput => _driver.FindElement(By.CssSelector("div.task_editor__input_fields  .public-DraftStyleDefault-block"));
        private IWebElement taskDescriptionTxtArea => _driver.FindElement(By.CssSelector("div.task_editor__input_fields textarea"));
        private IWebElement addTaskButton => _driver.FindElement(By.CssSelector("button[data-testid='task-editor-submit-button'] span"));
        private IWebElement deleteMenuOption => _driver.FindElement(By.CssSelector("li[data-action-hint='task-overflow-menu-delete']"));
        private IWebElement deleteConfirmButton => _driver.FindElement(By.CssSelector("button[type='submit'] span"));
        private IWebElement inboxButton => _driver.FindElement(By.Id("filter_inbox"));
        private IWebElement filterAndLabelButton => _driver.FindElement(By.Id("filters_labels"));
        private IWebElement openCommentBtn => _driver.FindElement(By.CssSelector("button[data-testid='open-comment-editor-button']"));
        private IWebElement commentTxtArea => _driver.FindElement(By.CssSelector("div.ProseMirror p"));
        private IWebElement addCommentBtn => _driver.FindElement(By.CssSelector("button[data-track='comments|add_comment']"));
        private IWebElement closeTaskWindowBtn => _driver.FindElement(By.CssSelector("button[aria-label='Close modal']"));

        private string todayTasksXpath = "//section[contains(@aria-label, 'Today')]";
        private string taskTitleXpath = "//li[@class='task_list_item']//div[text()='{0}']";
        private string threeDotsMenuXpath = "//button[@data-testid='more_menu']";
        private string editButtonXpath = "//div[text()='{0}']//ancestor::li//button[@data-action-hint='task-edit']";
        private string setDueDateXpath = "//div[text()='{0}']//ancestor::li//button[@class='due_date_controls']";
        private string dueDateOptionXpath = "//div[@class='scheduler-suggestions']//div[text()='{0}']";
        private string commentTxtXpath = "//div[@class='comments_list_container']//p[text()='{0}']";

        public TodayPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetHeaderTitle()
        {
            GetWebDriverWait(10).Until(e => e.FindElement(By.CssSelector("header h1")).Displayed);
            IWebElement headerTitle = _driver.FindElement(By.CssSelector("header h1"));

            return headerTitle.Text;
        }

        public void AddNewTask(string taskTitle, string taskDescription)
        {
            cmnElement.ClickElement(addTaskLnkButton);
            cmnElement.SetText(taskTitleInput, taskTitle);
            cmnElement.SetText(taskDescriptionTxtArea, taskDescription);
            cmnElement.ClickElement(addTaskButton);
        }

        public void DeleteTask(string taskTitle)
        {
            IWebElement tasksSection = _driver.FindElement(By.XPath(todayTasksXpath));
            IWebElement taskItem = tasksSection.FindElement(By.XPath(String.Format(taskTitleXpath, taskTitle)));

            WebDriverActions.MoveToElement(taskItem, _driver);
            GetWebDriverWait(10).Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format(editButtonXpath, taskTitle))));
            IWebElement moreMenu = taskItem.FindElement(By.XPath(threeDotsMenuXpath));

            cmnElement.ClickElement(moreMenu);
            cmnElement.ClickElement(deleteMenuOption);
            cmnElement.ClickElement(deleteConfirmButton);
        }

        public void EditTask(string taskTitle, string newTitle, string newDescription)
        {
            IWebElement taskItem = _driver.FindElement(By.XPath(String.Format(taskTitleXpath, taskTitle)));

            WebDriverActions.MoveToElement(taskItem, _driver);
            GetWebDriverWait(10).Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format(editButtonXpath, taskTitle)))).Click();

            cmnElement.DeleteText(taskTitleInput);
            cmnElement.DeleteText(taskDescriptionTxtArea);

            cmnElement.SetText(taskTitleInput, newTitle);
            cmnElement.SetText(taskDescriptionTxtArea, newDescription);

            cmnElement.ClickElement(addTaskButton);
        }

        public bool IsTaskCommentDisplayed(string taskTitle, string taskComment)
        {
            IWebElement taskItem = _driver.FindElement(By.XPath(String.Format(taskTitleXpath, taskTitle)));

            cmnElement.ClickElement(taskItem);

            return IsElementDisplayed(By.XPath(String.Format(commentTxtXpath, taskComment)), 10);

        }

        public void AddCommentToATask(string taskTitle, string taskComment)
        {
            IWebElement taskItem = _driver.FindElement(By.XPath(String.Format(taskTitleXpath, taskTitle)));

            cmnElement.ClickElement(taskItem);
            cmnElement.ClickElement(openCommentBtn);
            cmnElement.SetText(commentTxtArea, taskComment);
            cmnElement.ClickElement(addCommentBtn);
            cmnElement.ClickElement(closeTaskWindowBtn);
        }

        public void SetDueDate(string taskTitle, string dueDate)
        {
            IWebElement taskItem = _driver.FindElement(By.XPath(String.Format(taskTitleXpath, taskTitle)));

            WebDriverActions.MoveToElement(taskItem, _driver);
            GetWebDriverWait(10).Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format(setDueDateXpath, taskTitle)))).Click();

            GetWebDriverWait(10).Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format(dueDateOptionXpath, dueDate)))).Click();
        }

        public bool IsTaskItemDisplayed(string taskTitle)
        {
            return IsElementDisplayed(By.XPath(String.Format(taskTitleXpath, taskTitle)), 10);
        }

        public InboxPage GoToInboxPage()
        {
            cmnElement.ClickElement(inboxButton);

            return new InboxPage(_driver);
        }

        public FiltersAndLabelsPage GoToFilterAndLabelsPage()
        {
            cmnElement.ClickElement(filterAndLabelButton);

            return new FiltersAndLabelsPage(_driver);
        }
    }
}
