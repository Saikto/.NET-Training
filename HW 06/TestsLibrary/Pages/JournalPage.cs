using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class JournalPage
    {
        private IWebDriver _driver;

        public By ArticleOpenIndicatorBy = By.XPath(@"//*[@id=""ej-article-indicators-open""]");
        public By ArticleBy = By.XPath(@"//article");
        public By CurrentIssueBy = By.XPath(@"//*[@id=""wpCurrentIssue""]");

        private IWebElement ArticleOpenIndicator;
        private List<IWebElement> ArticleList;
        private IWebElement CurrentIssue;

        public JournalPage(IWebDriver driver)
        {
            _driver = driver;
            ArticleOpenIndicator = _driver.FindElement(ArticleOpenIndicatorBy);
            ArticleList = _driver.FindElements(ArticleBy).ToList();
            CurrentIssue = _driver.FindElement(CurrentIssueBy);
        }

        public IWebElement FindOpenArticle()
        {
            if (ArticleOpenIndicator != null)
            {
                return ArticleOpenIndicator;
            }
            throw new NoSuchElementException();
        }

        public IWebElement FindFreeArticle()
        {
            var articlesActionsList = ArticleList.Select(a => a.FindElement(By.XPath(@"//*[@class=""article-actions""]")));
            foreach (var article in articlesActionsList)
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
            throw new NoSuchElementException();
        }

        public void NavigateToCurrentIssue()
        {
            CurrentIssue.FindElement(By.TagName("a")).Click();
        }
    }
}
