using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class ArticlePage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = @"//*[@class=""content-box-body-list no-list-style""]")]
        private IWebElement ArticleMenu;
        
        public ArticlePage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public List<string> GetArticleMenu()
        {
            try
            {
                var listOfLi = ArticleMenu.FindElements(By.TagName("li"));
                var listOfActions = listOfLi.Select(t => t.FindElement(By.TagName("a")).GetAttribute("innerHTML")).ToList();
                return listOfActions;
            }
            catch (NoSuchElementException exception)
            {
                throw new NoSuchElementException("Article menu not found.", exception);
            }
        }
    }
}
