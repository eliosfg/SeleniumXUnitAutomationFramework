using CommonLibs.Implementation;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace TodoistApplication.Pages
{
    public class HomePage: BasePage
    {
        private IWebElement headerTitle => _driver.FindElement(By.CssSelector("header h1"));
        private IWebElement addTaskLnkButton => _driver.FindElement(By.CssSelector("button.plus_add_button"));
        private IWebElement taskTitleInput => _driver.FindElement(By.CssSelector("div.task_editor__input_fields  .public-DraftStyleDefault-block"));
        private IWebElement taskDescriptionTxtArea => _driver.FindElement(By.CssSelector("div.task_editor__input_fields textarea"));
        private IWebElement addTaskButton => _driver.FindElement(By.CssSelector("button[data-testid='task-editor-submit-button'] span"));
        private IWebElement deleteMenuOption => _driver.FindElement(By.CssSelector("li[data-action-hint='task-overflow-menu-delete']"));
        private IWebElement deleteConfirmButton => _driver.FindElement(By.CssSelector("button[type='submit'] span"));
        private IWebElement inboxButton => _driver.FindElement(By.Id("filter_inbox"));

        private string todayTasksXpath = "//section[contains(@aria-label, 'Today')]";
        private string taskTitleXpath = "//li[@class='task_list_item']//div[text()='{0}']";
        private string threeDotsMenuXpath = "//button[@data-testid='more_menu']";
        private string editButtonXpath = "//div[text()='{0}']//ancestor::li//button[@data-action-hint='task-edit']";
        private string setDueDateXpath = "//div[text()='{0}']//ancestor::li//button[@class='due_date_controls']";
        private string dueDateOptionXpath = "//div[@class='scheduler-suggestions']//div[text()='{0}']";

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetHeaderTitle()
        {
            getWebDriverWait(10).Until(e => e.FindElement(By.CssSelector("header h1")).Displayed);
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

            WebDriverActions.moveToElement(taskItem, _driver);
            Thread.Sleep(2000);
            IWebElement moreMenu = taskItem.FindElement(By.XPath(threeDotsMenuXpath));

            cmnElement.ClickElement(moreMenu);
            cmnElement.ClickElement(deleteMenuOption);
            cmnElement.ClickElement(deleteConfirmButton);
        }

        public void EditTask(string taskTitle, string newTitle, string newDescription)
        {
            IWebElement taskItem = _driver.FindElement(By.XPath(String.Format(taskTitleXpath, taskTitle)));

            WebDriverActions.moveToElement(taskItem, _driver);
            getWebDriverWait(10).Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format(editButtonXpath, taskTitle)))).Click();

            cmnElement.DeleteText(taskTitleInput);
            cmnElement.DeleteText(taskDescriptionTxtArea);

            cmnElement.SetText(taskTitleInput, newTitle);
            cmnElement.SetText(taskDescriptionTxtArea, newDescription);

            cmnElement.ClickElement(addTaskButton);
        }

        public void SetDueDate(string taskTitle, string dueDate)
        {
            IWebElement taskItem = _driver.FindElement(By.XPath(String.Format(taskTitleXpath, taskTitle)));

            WebDriverActions.moveToElement(taskItem, _driver);
            getWebDriverWait(10).Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format(setDueDateXpath, taskTitle)))).Click();

            getWebDriverWait(10).Until(ExpectedConditions.ElementIsVisible(By.XPath(String.Format(dueDateOptionXpath, dueDate)))).Click();
        }

        public bool IsTaskItemDisplayed(string taskTitle)
        {
            return isElementDisplayed(By.XPath(String.Format(taskTitleXpath, taskTitle)), 10);
        }

        public InboxPage GoToInboxPage()
        {
            cmnElement.ClickElement(inboxButton);

            return new InboxPage(_driver);
        }
    }
}
