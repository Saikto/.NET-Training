using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary
{
    public class JournalPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = @"//*[@id=""ej-article-indicators-open""]")]
        private IWebElement ArticleOpenIndicator;
        
        [FindsBy(How = How.XPath, Using = @"//*[@class=""article-list""]")]
        private IWebElement ArticleList;

        [FindsBy(How = How.Id, Using = "wpCurrentIssue")]
        private IWebElement CurrentIssue;

        public JournalPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public IWebElement FindOpenArticle()
        {
            return ArticleOpenIndicator;
        }

        public IWebElement FindFreeArticle()
        {
            var listOfArticles = ArticleList.FindElements(By.XPath(@"//*[@class=""article-actions""]"));
            foreach (var article in listOfArticles)
            {
                var listOfLi = article.FindElements(By.TagName("li"));
                foreach (var element in listOfLi)
                {
                    string id = element.GetAttribute("id");
                    if (id.EndsWith("liFree"))
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        public void NavigateToCurrentIssue()
        {
            CurrentIssue.FindElement(By.TagName("a")).Click();
        }
    }
}
