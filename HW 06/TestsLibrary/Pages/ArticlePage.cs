using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class ArticlePage
    {
        private IWebDriver _driver;

        public By articleMenuBy = By.XPath(@"//*[@class=""content-box-body-list no-list-style""]");
        public By viewImagesGalleryBy = By.XPath(@"//a[contains(@id, ""hypViewImagesGallery"")]");

        private IWebElement articleMenu;
        private IWebElement viewImagesGallery;

        public ArticlePage(IWebDriver driver)
        {
            _driver = driver;
            articleMenu = _driver.FindElement(articleMenuBy);
            viewImagesGallery = articleMenu.FindElement(viewImagesGalleryBy);
        }

        public List<IWebElement> GetArticleMenu()
        {
            try
            {
                var listOfArticleTools = articleMenu.FindElements(By.TagName("li")).ToList();
                return listOfArticleTools;
            }
            catch (NoSuchElementException exception)
            {
                throw new NoSuchElementException("Article menu not found.", exception);
            }
        }

        public void GoToImageGallery()
        {
            var imageGalleryLink = viewImagesGallery.GetAttribute("href");
            _driver.Url = imageGalleryLink;
        }
    }
}
