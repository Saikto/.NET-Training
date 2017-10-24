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
        private readonly IWebDriver _driver;

        public static By DeleteCollectionButtonBy => By.XPath(@"//a[contains(@id, ""lnkDeleteMyCollection"")]");
        public static By DeleteCollectionConfirmButtonBy => By.XPath(@"//input[contains(@name, ""deleteMyCollectionControl$btnDelete"")]");
        public static By MyFavoritesTabBy => By.XPath(@"//a[contains(@id, ""PlaceHolderMain_tabsControl1_hypMyFavorites"")]");

        private AcrticlesContainer ArticlesContainer => new AcrticlesContainer(_driver);
        private IWebElement MyFavoritesTab => _driver.FindElement(MyFavoritesTabBy);
        private IWebElement DeleteCollectionConfirmButton => _driver.FindElement(DeleteCollectionConfirmButtonBy);
        private IWebElement DeleteCollectionButton => _driver.FindElement(DeleteCollectionButtonBy);



        public MyAccountPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoToFavoritesTab()
        {
            MyFavoritesTab.Click();
        }

        public List<string> GetFavoritesLinksFromFolder(string folder)
        {
            _driver.FindElement(By.LinkText(folder)).Click();
            var listOfHrefs = ArticlesContainer.GetArticlesList().Select(a => a.Href).ToList();
            return listOfHrefs;
        }

        public void DeleteCurrentFolder()
        {
            DeleteCollectionButton.Click();
        }

        public void DeleteCurrentFolderConfirm()
        {
            DeleteCollectionConfirmButton.Click();
        }
    }
}
