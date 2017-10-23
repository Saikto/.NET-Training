using OpenQA.Selenium;

namespace TestsLibrary.Models
{
    public class Article
    {
        private IWebDriver _driver;
        public static By ArticleActionsBy = By.XPath(@".//*[@class=""article-actions""]");
        public By ArticleOpenIndicatorBy = By.XPath(@".//*[@id=""ej-article-indicators-open""]");
        public By ArticleFreeIndicatorBy = By.XPath(@".//li[contains(@id, ""liFree"")]");
        public By ArticlePapIndicatorBy = By.XPath(@".//li[contains(@id, ""liPAP"")]");
        //public static By HeaderBy = By.XPath(@".//div[1]/div[1]/header[1]/h4[1]/a[1]");
        public static By HeaderBy = By.XPath(@".//header[1]/h4[1]/a[1]");
        public static By ArticleAddtoFavoritesBy = By.XPath(@".//li[contains(@id,""liAddToMyCollections"")]");

        public string Title;
        public string Href;
        public IWebElement ArticleOpenIndicator;
        public IWebElement ArticleFreeIndicator;
        public IWebElement ArticlePapIndicator;
        private IWebElement ArticleAddtoFavorites;
        private IWebElement ArticleActions;
        private IWebElement article;

        public Article(IWebElement article)
        {
            this.article = article;
            Title = article.FindElement(HeaderBy).GetAttribute("title");
            Href = article.FindElement(HeaderBy).GetAttribute("href");
            if (!Href.Contains("imagegallery"))
            {
                ArticleActions = article.FindElement(ArticleActionsBy);
            }
        }

        public bool IsPap()
        {
            try
            {
                ArticlePapIndicator = ArticleActions.FindElement(ArticlePapIndicatorBy);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;

        }

        public bool IsFreeArticle()
        {
            try
            {
                ArticleFreeIndicator = ArticleActions.FindElement(ArticleFreeIndicatorBy);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;

        }

        public bool IsOpen()
        {
            try
            {
                ArticleOpenIndicator = ArticleActions.FindElement(ArticleOpenIndicatorBy);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;
        }

        public void AddToFavorites()
        {
            ArticleAddtoFavorites = article.FindElement(ArticleAddtoFavoritesBy);
            ArticleAddtoFavorites.Click();
        }
    }
}
