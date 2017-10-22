using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TestsLibrary.Models;
using TestsLibrary.Utils;

namespace TestsLibrary.Pages
{
    public class AcrticlesContainer
    {
        private IWebDriver _driver;

        public static By ArticlesOuterBy = By.XPath(@"//div[@class=""wp-feature-articles""]");
        public static By ArticleBy = By.XPath(@"//article");

        private List<Article> ArticleList;
        private IWebElement ArticlesOuter;

        public AcrticlesContainer(IWebDriver driver)
        {
            _driver = driver;
            ArticlesOuter = _driver.FindElement(ArticlesOuterBy);
            ArticleList = new List<Article>();
            ArticleList = ArticlesOuter.FindElements(ArticleBy).ToList().Select(a => new Article(a)).ToList();
        }

        public IWebElement FindFreeOrOpenArticle()
        {
            foreach (var article in ArticleList)
            {
                if (article.IsFreeArticle())
                    return article.ArticleFreeIndicator;
                if (article.IsOpen())
                    return article.ArticleOpenIndicator;
            }
            return null;
        }

        public List<Article> GetArticlesList()
        {
            return ArticleList;
        }

        public void AddArticleToFavorites(Article article)
        {
            if (!ArticleList.Contains(article))
            {
                throw new ArgumentException("No such article found.");
            }
            article.AddToFavorites();
        }

    }
}
