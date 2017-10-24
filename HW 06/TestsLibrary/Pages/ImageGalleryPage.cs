using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace TestsLibrary.Pages
{
    public class ImageGalleryPage
    {
        private readonly IWebDriver _driver;

        public static By ActionsDropDownMenuPpBy = By.XPath(@"//*[@class = ""toolbar-nav toolbar-nav-left""]/ul[1]/li[3]/a[1]");
        public static By ThumbDetailsElementsBy = By.XPath(@"//*[@class = ""ej-thumb-details""]");
        public static By SelectAllCheckBoxBy = By.XPath(@"//*[@id = ""chkSelectTop""]");
        public static By ActionsDropDownListToggleBy = By.XPath(@"//*[@class = ""toolbar-nav toolbar-nav-left""]/li[1]");
        public static By ActionsDropDownListMenuBy = By.XPath(@"//*[@class = ""wk--toolbar__dropdown-menu""]");


        private IWebElement ThumbDetailsElements => _driver.FindElement(ThumbDetailsElementsBy);
        private IWebElement SelectAllCheckBox => _driver.FindElement(SelectAllCheckBoxBy);
        private IWebElement ActionsDropDownListToggle => _driver.FindElement(ActionsDropDownListToggleBy);


        public ImageGalleryPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public List<IWebElement> GetImagesLinks()
        {
            var listOfLinks = ThumbDetailsElements.FindElements(By.XPath("//li/div[1]/a[1]")).ToList();
            return listOfLinks;
        }

        public void ExportSelectedToPowerPoint()
        {
            ActionsDropDownListToggle.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(ActionsDropDownMenuPpBy, "Export to PowerPoint"));
            try
            {
                _driver.ExecuteJavaScript(@"ItemListActionsControl_DoAction('exportToPowerPoint', this);");
            }
            catch
            {
            }
        }

        public void SelectAllImages()
        {
            SelectAllCheckBox.Click();
        }
    }
}
