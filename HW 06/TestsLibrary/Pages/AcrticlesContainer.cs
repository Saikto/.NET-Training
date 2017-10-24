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
        private readonly IWebDriver _driver;

        public static By ArticleBy = By.XPath(@"//article");

        private List<Article> ArticleList => _driver.FindElements(ArticleBy).ToList().Where(a => a.Text != "").Select(a => new Article(a)).ToList();

        public AcrticlesContainer(IWebDriver driver)
        {
            _driver = driver;
        }

        public List<Article> GetArticlesList()
        {
            return ArticleList;
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

        public void AddArticleToFavorites(Article article)
        {
            //if (!ArticleList.Contains(article))
            //{
            //    throw new ArgumentException("No such article found.");
            //}
            article.AddToFavorites();
        }

    }
}
