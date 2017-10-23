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
        private IWebDriver _driver;

        public static By actionsDropDownMenuPpBy = By.XPath(@"//*[@class = ""toolbar-nav toolbar-nav-left""]/ul[1]/li[3]/a[1]");
        public static By thumbDetailsElementsBy = By.XPath(@"//*[@class = ""ej-thumb-details""]");
        public static By selectAllCheckBoxBy = By.XPath(@"//*[@id = ""chkSelectTop""]");
        public static By actionsDropDownListToggleBy = By.XPath(@"//*[@class = ""toolbar-nav toolbar-nav-left""]/li[1]");
        public static By actionsDropDownListMenuBy = By.XPath(@"//*[@class = ""wk--toolbar__dropdown-menu""]");


        private IWebElement thumbDetailsElements;
        private IWebElement selectAllCheckBox;
        private IWebElement actionsDropDownListToggle;


        public ImageGalleryPage(IWebDriver driver)
        {
            _driver = driver;
            thumbDetailsElements = _driver.FindElement(thumbDetailsElementsBy);
            selectAllCheckBox = _driver.FindElement(selectAllCheckBoxBy);
            actionsDropDownListToggle = _driver.FindElement(actionsDropDownListToggleBy);
        }

        public List<IWebElement> GetImagesLinks()
        {
            var listOfLinks = thumbDetailsElements.FindElements(By.XPath("//li/div[1]/a[1]")).ToList();
            return listOfLinks;
        }

        public void ExportSelectedToPowerPoint()
        {
            actionsDropDownListToggle.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(actionsDropDownMenuPpBy, "Export to PowerPoint"));
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
            selectAllCheckBox.Click();
        }
    }
}
