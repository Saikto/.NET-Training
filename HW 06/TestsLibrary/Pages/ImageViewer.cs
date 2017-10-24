using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class ImageViewer
    {
        private readonly IWebDriver _driver;
        
        public static By ImageModalBy => By.XPath(@"//*[@id=""ArticleImageModal""]");
        public static By ImageViewBy => By.XPath("//div[1]/section[1]/div[1]/div[1]/img[1]");
        public static By ImageDisplayedCounterBy => By.XPath("//div/section/div[2]/div/div[2]/span");
        public static By NextImageButtonBy => By.XPath("//div/section/div[2]/div/div[2]/a[2]");

        private IWebElement ImageModal => _driver.FindElement(ImageModalBy);
        private IWebElement ImageView => ImageModal.FindElement(ImageViewBy);
        private IWebElement ImageDisplayedCounter => ImageModal.FindElement(ImageDisplayedCounterBy);
        private IWebElement NextImageButton => ImageModal.FindElement(NextImageButtonBy);

        public ImageViewer(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetCurrentImageLink()
        {
            return ImageView.GetAttribute("src");
        }

        public int GetImagesCount()
        {
            string s = ImageDisplayedCounter.Text;
            int count = Int32.Parse(s.Split()[2]);
            return count;
        }

        public int GetCurrentImageNumber()
        {
            string s = ImageDisplayedCounter.Text;
            int currentNumber = Int32.Parse(s.Split()[0]);
            return currentNumber;
        }

        public void NextImage()
        {
            NextImageButton.Click();
        }
    }

 
}
