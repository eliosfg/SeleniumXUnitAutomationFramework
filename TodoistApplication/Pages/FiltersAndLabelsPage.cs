using OpenQA.Selenium;
using System;

namespace TodoistApplication.Pages
{
    public class FiltersAndLabelsPage : BasePage
    {
        private IWebElement addLabelBtn => _driver.FindElement(By.CssSelector("button[aria-label='Add new label']"));
        private IWebElement labelNameTxtInput => _driver.FindElement(By.Id("edit_label_modal_field_name"));
        private IWebElement labelColorBtn => _driver.FindElement(By.ClassName("color_dropdown_toggle"));
        private IWebElement addLabelRedBtn => _driver.FindElement(By.ClassName("ist_button_red"));
        private IWebElement goBackBtn => _driver.FindElement(By.ClassName("view_header__previous_view"));

        private string labelColorOptXpath = "//span[text()='{0}']";
        private string labelTitleXpath = "//h1//span[text()='{0}']";
        private string labelItemXpath = "//span[text()='{0}']//ancestor::li";

        public FiltersAndLabelsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AddNewLabel(string labelName, string labelColor)
        {
            cmnElement.ClickElement(addLabelBtn);
            cmnElement.SetText(labelNameTxtInput, labelName);
            cmnElement.ClickElement(labelColorBtn);

            WaitAndFindElement(By.XPath(String.Format(labelColorOptXpath, labelColor))).Click();
            cmnElement.ClickElement(addLabelRedBtn);
        }

        public bool IsLabelHeaderDisplayed(string labelName)
        {
            return IsElementDisplayed(By.XPath(String.Format(labelTitleXpath, labelName)), 10);
        }

        public void GoToMainView()
        {
            cmnElement.ClickElement(goBackBtn);
        }

        public bool IsLabelItemDisplayed(string labelName)
        {
            return IsElementDisplayed(By.XPath(String.Format(labelItemXpath, labelName)), 10);
        }
    }
}
