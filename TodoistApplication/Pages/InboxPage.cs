using OpenQA.Selenium;
using System;

namespace TodoistApplication.Pages
{
    public class InboxPage : BasePage
    {
        private IWebElement taskTitleInput => _driver.FindElement(By.CssSelector("div.task_editor__input_fields  .public-DraftStyleDefault-block"));
        private IWebElement taskDescriptionTxtArea => _driver.FindElement(By.CssSelector("div.task_editor__input_fields textarea"));
        private IWebElement addTaskButton => _driver.FindElement(By.CssSelector("button[data-testid='task-editor-submit-button'] span"));

        private string taskItemXpath = "//li[contains(@class, 'task_list_item')]//div[text()='{0}']";

        public InboxPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsTaskItemDisplayed(string taskTitle)
        {
            return isElementDisplayed(By.XPath(String.Format(taskItemXpath, taskTitle)), 10);
        }
    }
}
