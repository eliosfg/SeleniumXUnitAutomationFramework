using OpenQA.Selenium;
using System;

namespace TodoistApplication.Pages
{
    public class InboxPage : BasePage
    {
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
