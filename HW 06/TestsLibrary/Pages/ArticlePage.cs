using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class ArticlePage
    {
        private readonly IWebDriver _driver;

        public static By ArticleMenuBy = By.XPath(@"//*[@class=""content-box-body-list no-list-style""]");
        public static By ViewImagesGalleryBy = By.XPath(@"//a[contains(@id, ""hypViewImagesGallery"")]");
        public static By AddToMyCollectionsIconBy = By.XPath(@"//i[@class=""wki icon-favorites""]");

        private IWebElement ArticleMenu => _driver.FindElement(ArticleMenuBy);
        private IWebElement ViewImagesGallery => ArticleMenu.FindElement(ViewImagesGalleryBy);

        public ArticlePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public List<IWebElement> GetArticleMenu()
        {
            try
            {
                var listOfArticleTools = ArticleMenu.FindElements(By.TagName("li")).ToList();
                return listOfArticleTools;
            }
            catch (NoSuchElementException exception)
            {
                throw new NoSuchElementException("Article menu not found.", exception);
            }
        }

        public void GoToImageGallery()
        {
            var imageGalleryLink = ViewImagesGallery.GetAttribute("href");
            _driver.Url = imageGalleryLink;
        }
    }
}
