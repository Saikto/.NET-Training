using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Pages;
using TestsLibrary.Utils;

namespace Tests.Task_3
{
    public class Tests_Task3_1 : SetUp_Task3
    {
        public Tests_Task3_1(string profile, string environment) : base(profile, environment) { }

        private static string StartUrl;
        private static string Login;
        private static string Pass;
        private static int ExpectedFileSize;
        private static string FilePath;
        private static string FolderName;

        [Test]
        public void TstOpenFristImage()
        {
            StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
            Driver.Url = StartUrl;
            ArticlePage articlePage = new ArticlePage(Driver);
            articlePage.GoToImageGallery();
            Wait.Until(ExpectedConditions.ElementIsVisible(ImageGalleryPage.ActionsDropDownListToggleBy));
            ImageGalleryPage galleryPage = new ImageGalleryPage(Driver);
            var imagesLinks = galleryPage.GetImagesLinks();
            imagesLinks[0].Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.ImageViewBy));
            bool a = true;
            Assert.IsTrue(a);
        }

        [Test]
        public void TstCheckImagesIteration()
        {
            StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
            Driver.Url = StartUrl;
            ArticlePage articlePage = new ArticlePage(Driver);
            articlePage.GoToImageGallery();
            if (DriverUtils.isAlertPresent(Driver))
                Driver.SwitchTo().Alert().Accept();
            Wait.Until(ExpectedConditions.ElementIsVisible(ImageGalleryPage.ActionsDropDownListToggleBy));
            ImageGalleryPage galleryPage = new ImageGalleryPage(Driver);
            var imagesLinks = galleryPage.GetImagesLinks();
            imagesLinks[0].Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.ImageViewBy));
            ImageViewer viewer = new ImageViewer(Driver);
            string firstImageLink = viewer.GetCurrentImageLink();
            int countV = viewer.GetImagesCount();
            if (countV != imagesLinks.Count)
                Assert.Fail();
            for (int i = 1; i <= imagesLinks.Count; i++)
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.ImageViewBy));
                int current = viewer.GetCurrentImageNumber();
                if (current != i)
                    Assert.Fail();
                viewer.NextImage();
            }
            Wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.ImageViewBy));
            if(viewer.GetCurrentImageLink() != firstImageLink)
                Assert.Fail();
        }

        [Test]
        public void TstCheckImagesDownload()
        {
            StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
            ExpectedFileSize = 948;
            FilePath = @"C:\Users\igor_\Downloads\image_download.pptx";
            //FilePath = @"C:\Users\igor_\Downloads\image_download.pptx";
            Driver.Url = StartUrl;
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(ArticlePage.ViewImagesGalleryBy));
            ArticlePage articlePage = new ArticlePage(Driver);
            articlePage.GoToImageGallery();
            Wait.Until(ExpectedConditions.ElementIsVisible(ImageGalleryPage.ActionsDropDownListToggleBy));
            ImageGalleryPage galleryPage = new ImageGalleryPage(Driver);
            galleryPage.SelectAllImages();
            galleryPage.ExportSelectedToPowerPoint();
            System.Threading.Thread.Sleep(5000);
            int size = FileSize.GetFileSizeInKB(FilePath);
            Assert.AreEqual(ExpectedFileSize, size, 1);
        }
    }
}
