using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
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
            if (ArticleOpenIndicator != null)
            {
                return ArticleOpenIndicator;
            }
            throw new NoSuchElementException();
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
            throw new NoSuchElementException();
        }

        public void NavigateToCurrentIssue()
        {
            CurrentIssue.FindElement(By.TagName("a")).Click();
        }
    }
}
