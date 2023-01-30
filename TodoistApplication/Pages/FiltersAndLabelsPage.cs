using CommonLibs.Implementation;
using OpenQA.Selenium;
using System;

namespace TodoistApplication.Pages
{
    public class FiltersAndLabelsPage : BasePage
    {
        private IWebElement addLabelBtn => _driver.FindElement(By.CssSelector("button[aria-label='Add new label']"));
        private IWebElement addFilterBtn => _driver.FindElement(By.CssSelector("button[aria-label='Add new filter']"));
        private IWebElement labelNameTxtInput => _driver.FindElement(By.Id("edit_label_modal_field_name"));
        private IWebElement dropdownColorBtn => _driver.FindElement(By.ClassName("color_dropdown_toggle"));
        private IWebElement addRedBtn => _driver.FindElement(By.ClassName("ist_button_red"));
        private IWebElement goBackBtn => _driver.FindElement(By.ClassName("view_header__previous_view"));
        private IWebElement confirmDeleteBtn => _driver.FindElement(By.CssSelector("button[type='submit'] span"));
        private IWebElement deleteLabelBtn => _driver.FindElement(By.CssSelector("li[data-track='labels|menu_delete']"));
        private IWebElement deleteFilterBtn => _driver.FindElement(By.CssSelector("li[data-track='filters|menu_delete']"));
        private IWebElement filterNameTxtInput => _driver.FindElement(By.Id("edit_filter_modal_field_name"));
        private IWebElement filterQueryTxtInput => _driver.FindElement(By.Id("edit_filter_modal_field_query"));

        private string dropdownColorOptXpath = "//span[text()='{0}']";
        private string labelFilterTitleXpath = "//h1//span[text()='{0}']";
        private string labelItemXpath = "//section[@aria-label='Labels']//span[text()='{0}']//ancestor::li";
        private string labelMoreMenuBtnXpath = "//section[@aria-label='Labels']//span[text()='{0}']//ancestor::li//button[@class='SidebarListItem__button']";
        private string labelEditBtnXpath = "//span[text()='{0}']//ancestor::li//button[@aria-label='Edit label']";
        private string filterItemXpath = "//section[@aria-label='Filters']//span[text()='{0}']//ancestor::li";
        private string filterMoreMenuBtnXpath = "//section[@aria-label='Filters']//span[text()='{0}']//ancestor::li//button[@class='SidebarListItem__button']";
        private string colorLabelCss = ".color_dropdown_select__name";

        public FiltersAndLabelsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AddNewLabel(string labelName, string labelColor)
        {
            cmnElement.ClickElement(addLabelBtn);
            cmnElement.SetText(labelNameTxtInput, labelName);
            cmnElement.ClickElement(dropdownColorBtn);

            WaitAndFindElement(By.XPath(String.Format(dropdownColorOptXpath, labelColor))).Click();
            cmnElement.ClickElement(addRedBtn);
        }

        public bool IsHeaderTitleDisplayed(string labelName)
        {
            return IsElementDisplayed(By.XPath(String.Format(labelFilterTitleXpath, labelName)), 10);
        }

        public void GoToMainView()
        {
            cmnElement.ClickElement(goBackBtn);
        }

        public bool IsLabelItemDisplayed(string labelName)
        {
            return IsElementDisplayed(By.XPath(String.Format(labelItemXpath, labelName)), 10);
        }

        public bool IsFilterItemDisplayed(string filterName)
        {
            return IsElementDisplayed(By.XPath(String.Format(filterItemXpath, filterName)), 10);
        }

        public void DeleteLabel(string labelName)
        {
            WebDriverActions.MoveToElement(WaitAndFindElement(By.XPath(String.Format(labelItemXpath, labelName))), _driver);

            WaitAndFindElement(By.XPath(String.Format(labelMoreMenuBtnXpath, labelName))).Click();
            cmnElement.ClickElement(deleteLabelBtn);
            cmnElement.ClickElement(confirmDeleteBtn);
        }

        public void AddNewFilter(string filterName, string filterQuery, string filterColor)
        {
            cmnElement.ClickElement(addFilterBtn);
            cmnElement.SetText(filterNameTxtInput, filterName);
            cmnElement.SetText(filterQueryTxtInput, filterQuery);
            cmnElement.ClickElement(dropdownColorBtn);

            WaitAndFindElement(By.XPath(String.Format(dropdownColorOptXpath, filterColor))).Click();
            cmnElement.ClickElement(addRedBtn);
        }

        public void EditLabel(string labelName, string newLabelName, string newLabelColor)
        {
            IWebElement labelItem = _driver.FindElement(By.XPath(String.Format(labelItemXpath, labelName)));
            WebDriverActions.MoveToElement(labelItem, _driver);

            WaitAndFindElement(By.XPath(String.Format(labelEditBtnXpath, labelName))).Click();

            cmnElement.ClearText(labelNameTxtInput);
            cmnElement.SetText(labelNameTxtInput, newLabelName);
            cmnElement.ClickElement(dropdownColorBtn);
            cmnElement.ClickElement(WaitAndFindElement(By.XPath(String.Format(dropdownColorOptXpath, newLabelColor))));
            cmnElement.ClickElement(addRedBtn);
        }

        public string GetLabelColor(string labelName)
        {
            IWebElement labelItem = _driver.FindElement(By.XPath(String.Format(labelItemXpath, labelName)));
            WebDriverActions.MoveToElement(labelItem, _driver);

            WaitAndFindElement(By.XPath(String.Format(labelEditBtnXpath, labelName))).Click();

            return WaitAndFindElement(By.CssSelector(colorLabelCss)).Text;
        }

        public void DeleteFilter(string filterName)
        {
            WebDriverActions.MoveToElement(WaitAndFindElement(By.XPath(String.Format(filterItemXpath, filterName))), _driver);

            WaitAndFindElement(By.XPath(String.Format(filterMoreMenuBtnXpath, filterName))).Click();
            cmnElement.ClickElement(deleteFilterBtn);
            cmnElement.ClickElement(confirmDeleteBtn);
        }
    }
}
