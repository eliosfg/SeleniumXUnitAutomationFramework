using CommonLibs.Implementation;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace TodoistApplication.Pages
{
    public class InboxPage : BasePage
    {
        private IWebElement taskTitleInput => _driver.FindElement(By.CssSelector("div.task_editor__input_fields  .public-DraftStyleDefault-block"));
        private IWebElement taskDescriptionTxtArea => _driver.FindElement(By.CssSelector("div.task_editor__input_fields textarea"));
        private IWebElement addTaskButton => _driver.FindElement(By.CssSelector("button[data-testid='task-editor-submit-button'] span"));
        private IWebElement closeTaskButton => _driver.FindElement(By.CssSelector("button[aria-label='Close modal']"));
        private IWebElement deleteMenuOption => _driver.FindElement(By.CssSelector("li[data-action-hint='task-overflow-menu-delete']"));
        private IWebElement deleteConfirmButton => _driver.FindElement(By.CssSelector("button[type='submit'] span"));
        private IWebElement duplicateMenuOption => _driver.FindElement(By.CssSelector("li[data-action-hint='task-overflow-menu-duplicate']"));
        private IWebElement commentBtn => _driver.FindElement(By.CssSelector("a[aria-label='Comments']"));
        private IWebElement commentTxtArea => _driver.FindElement(By.CssSelector("p[aria-placeholder='Comment']"));
        private IWebElement addCommentBtn => _driver.FindElement(By.CssSelector("button[data-track='comments|add_comment']"));
        private IWebElement closeCommentWindowBtn => _driver.FindElement(By.ClassName("project_detail_close"));

        private string taskItemXpath = "//li[contains(@class, 'task_list_item')]//div[text()='{0}']";
        private string addTaskLnkBtnXpath = "//span[text()='{0}']//ancestor::li//button[@class='plus_add_button']";
        private string moreMenuBtnXpath = "//div[text()='{0}']//ancestor::div[@class='task_list_item__body']//button[@data-testid='more_menu']";
        private string priorityOptionsCss = ".priority_list svg[data-priority='{0}']";
        private string prioritySelectorCss = "div[aria-label='Priority'] span";
        private string commentTextContentXpath = "//p[text()='{0}']";

        public InboxPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public int GetTasksCount(string taskTitle)
        {
            IReadOnlyList<IWebElement> tasksList = _driver.FindElements(By.XPath(String.Format(taskItemXpath, taskTitle)));

            return tasksList.Count;
        }

        public void AddNewPriorityTask(string taskTitle, string taskDescription, string priority)
        {
            IWebElement addTaskLnkBtn = _driver.FindElement(By.XPath(String.Format(addTaskLnkBtnXpath, priority)));
            cmnElement.ClickElement(addTaskLnkBtn);
            cmnElement.SetText(taskTitleInput, taskTitle);
            cmnElement.SetText(taskDescriptionTxtArea, taskDescription);
            cmnElement.ClickElement(addTaskButton);
        }

        public void ChangeTaskPriority(string taskTitle, string newPriority)
        {
            IWebElement taskItem = WaitAndFindElement(By.XPath(String.Format(taskItemXpath, taskTitle)));

            WebDriverActions.MoveToElement(taskItem, _driver);
            WaitAndFindElement(By.XPath(String.Format(moreMenuBtnXpath, taskTitle))).Click();
            WaitAndFindElement(By.CssSelector(String.Format(priorityOptionsCss, newPriority))).Click();
        }

        public string GetTaskPriority(string taskTitle)
        {
            WaitAndFindElement(By.XPath(String.Format(taskItemXpath, taskTitle))).Click();

            string taskPriority = WaitAndFindElement(By.CssSelector(prioritySelectorCss)).Text;

            cmnElement.ClickElement(closeTaskButton);

            return taskPriority;
        }

        public void DuplicateTask(string taskTitle)
        {
            IWebElement taskItem = _driver.FindElement(By.XPath(String.Format(taskItemXpath, taskTitle)));

            WebDriverActions.MoveToElement(taskItem, _driver);
            WaitAndFindElement(By.XPath(String.Format(moreMenuBtnXpath, taskTitle))).Click();
            cmnElement.ClickElement(duplicateMenuOption);
        }

        public void DeleteTask(string taskTitle)
        {
            IWebElement taskItem = _driver.FindElement(By.XPath(String.Format(taskItemXpath, taskTitle)));

            WebDriverActions.MoveToElement(taskItem, _driver);
            WaitAndFindElement(By.XPath(String.Format(moreMenuBtnXpath, taskTitle))).Click();
            cmnElement.ClickElement(deleteMenuOption);
            cmnElement.ClickElement(deleteConfirmButton);
        }

        public bool IsTaskItemDisplayed(string taskTitle)
        {
            return IsElementDisplayed(By.XPath(String.Format(taskItemXpath, taskTitle)), 10);
        }

        public void AddCommentToInbox(string comment)
        {
            cmnElement.ClickElement(commentBtn);
            cmnElement.SetText(commentTxtArea, comment);
            cmnElement.ClickElement(addCommentBtn);
            cmnElement.ClickElement(closeCommentWindowBtn);
        }

        public bool IsCommentDisplayed(string comment)
        {
            cmnElement.ClickElement(commentBtn);

            return IsElementDisplayed(By.XPath(String.Format(commentTextContentXpath, comment)), 10);
        }
    }
}
