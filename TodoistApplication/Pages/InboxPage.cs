using CommonLibs.Implementation;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;

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

        private string taskItemXpath = "//li[contains(@class, 'task_list_item')]//div[text()='{0}']";
        private string addTaskLnkBtnXpath = "//span[text()='{0}']//ancestor::li//button[@class='plus_add_button']";
        private string moreMenuBtnXpath = "//div[text()='{0}']//ancestor::div[@class='task_list_item__body']//button[@data-testid='more_menu']";
        private string priorityOptionsCss = ".priority_list svg[data-priority='{0}']";
        private string prioritySelectorCss = "div[aria-label='Priority'] span";

        public InboxPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsTaskItemDisplayed(string taskTitle)
        {
            return IsElementDisplayed(By.XPath(String.Format(taskItemXpath, taskTitle)), 10);
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

        public void DeleteTask(string taskTitle)
        {
            IWebElement taskItem = _driver.FindElement(By.XPath(String.Format(taskItemXpath, taskTitle)));

            WebDriverActions.MoveToElement(taskItem, _driver);
            WaitAndFindElement(By.XPath(String.Format(moreMenuBtnXpath, taskTitle))).Click();
            cmnElement.ClickElement(deleteMenuOption);
            cmnElement.ClickElement(deleteConfirmButton);
        }
    }
}
