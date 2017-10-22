using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Utils;

namespace TestsLibrary.Pages
{
    public class MyAccountPage
    {
        private IWebDriver _driver;

        public static By deleteCollectionButtonBy => By.XPath(@"//a[contains(@id, ""lnkDeleteMyCollection"")]");
        public static By deleteCollectionConfirmButtonBy => By.XPath(@"//input[contains(@name, ""deleteMyCollectionControl$btnDelete"")]");
        public static By myFavoritesTabBy => By.XPath(@"//a[contains(@id, ""PlaceHolderMain_tabsControl1_hypMyFavorites"")]");

        private AcrticlesContainer ArticlesContainer;
        private IWebElement myFavoritesTab;
        private IWebElement deleteCollectionConfirmButton;
        private IWebElement deleteCollectionButton;
        


        public MyAccountPage(IWebDriver driver)
        {
            _driver = driver;
            myFavoritesTab = _driver.FindElement(myFavoritesTabBy);
            
        }

        public void GoToFavoritesTab()
        {
            myFavoritesTab.Click();
            ArticlesContainer = new AcrticlesContainer(_driver);
        }

        public List<string> GetFavoritesLinksFromFolder(string folder)
        {
            _driver.FindElement(By.LinkText(folder)).Click();
            System.Threading.Thread.Sleep(3000);
            ArticlesContainer = new AcrticlesContainer(_driver);
            var listOfHrefs = ArticlesContainer.GetArticlesList().Select(a => a.Href).ToList();
            return listOfHrefs;
        }

        public void DeleteCurrentFolder()
        {
            deleteCollectionButton = _driver.FindElement(deleteCollectionButtonBy);
            deleteCollectionButton.Click();
        }

        public void DeleteCurrentFolderConfirm()
        {
            deleteCollectionConfirmButton = _driver.FindElement(deleteCollectionConfirmButtonBy);
            deleteCollectionConfirmButton.Click();
        }
    }
}
