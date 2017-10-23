using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class ImageViewer
    {
        private IWebDriver _driver;
        
        public static By imageModalBy => By.XPath(@"//*[@id=""ArticleImageModal""]");
        public static By imageViewBy => By.XPath("//div[1]/section[1]/div[1]/div[1]/img[1]");
        public static By imageDisplayedCounterBy => By.XPath("//div/section/div[2]/div/div[2]/span");
        public static By nextImageButtonBy => By.XPath("//div/section/div[2]/div/div[2]/a[2]");

        private IWebElement imageModal;
        private IWebElement imageView;
        private IWebElement imageDisplayedCounter;
        private IWebElement nextImageButton;

        public ImageViewer(IWebDriver driver)
        {
            _driver = driver;
            imageModal = _driver.FindElement(imageModalBy);
            imageDisplayedCounter = imageModal.FindElement(imageDisplayedCounterBy);
            nextImageButton = imageModal.FindElement(nextImageButtonBy);
        }

        public IWebElement GetImageView()
        {
            imageView = imageModal.FindElement(imageViewBy);
            return imageView;
        }

        public int GetImagesCount()
        {
            string s = imageDisplayedCounter.Text;
            int count = Int32.Parse(s.Split()[2]);
            return count;
        }

        public int GetCurrentImageNumber()
        {
            string s = imageDisplayedCounter.Text;
            int currentNumber = Int32.Parse(s.Split()[0]);
            return currentNumber;
        }

        public void NextImage()
        {
            nextImageButton.Click();
        }
    }

 
}
