using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class MyAccountPage
    {
        private IWebDriver _driver;

        public By deleteCollectionButtonBy => By.XPath(@"//a[contains(@id, ""lnkDeleteMyCollection"")]");
        public By deleteCollectionConfirmButtonBy => By.XPath(@"//input[contains(@id, ""deleteMyCollectionControl$btnDelete"")]");
        public By myFavoritesTabBy => By.XPath(@"//a[contains(@id, ""PlaceHolderMain_tabsControl1_hypMyFavorites"")]");

        private IWebElement myFavoritesTab;
        private IWebElement deleteCollectionConfirmButton;
        private IWebElement deleteCollectionButton;
        


        public MyAccountPage(IWebDriver driver)
        {
            _driver = driver;
            myFavoritesTab = _driver.FindElement(myFavoritesTabBy);
            deleteCollectionButton = _driver.FindElement(deleteCollectionButtonBy);
        }

        public void GoToFavoritesTab()
        {
            myFavoritesTab.Click();
        }

        public List<string> GetFavoritesFromFolder(string folder)
        {
            _driver.FindElement(By.LinkText(folder)).Click();
            var listOfarticles = _driver.FindElements(By.XPath("//article"));
            var listOfHrefs = listOfarticles.Select(a =>
                a.FindElement(By.XPath("//div[1]/div[1]/header[1]/h4[1]/a[1]")).GetAttribute("href")).ToList();
            return listOfHrefs;
        }

        public void DeleteCurrentFolder()
        {
            deleteCollectionButton.Click();
            deleteCollectionConfirmButton.Click();
        }
    }
}
