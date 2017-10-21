using System;
using NUnit.Framework;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Pages;

namespace Tests.Task_3
{
    public class TestsTask3
    {
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstOpenFristImage()
        {
            //TEST SETUP//
            var driver = TestDataTask3.DataForTstOpenFristImage.Driver;
            driver.Url = TestDataTask3.DataForTstOpenFristImage.StartUrl;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //TEST
            using (driver)
            {
            ArticlePage articlePage = new ArticlePage(driver);
            articlePage.GoToImageGallery();
            ImageGalleryPage galleryPage = new ImageGalleryPage(driver);
            var imagesLinks = galleryPage.GetImagesLinks();
            imagesLinks[0].Click();
            ImageViewer viewer = new ImageViewer(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(viewer.imageViewBy));
            bool a = true;
            Assert.IsTrue(a);
            }
        }

        [Test]
        public void TstCheckImagesIteration()
        {
            //TEST SETUP//
            var driver = TestDataTask3.DataForTstCheckImagesIteration.Driver;
            driver.Url = TestDataTask3.DataForTstCheckImagesIteration.StartUrl;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //TEST
            using (driver)
            {
                ArticlePage articlePage = new ArticlePage(driver);
                articlePage.GoToImageGallery();
                ImageGalleryPage galleryPage = new ImageGalleryPage(driver);
                var imagesLinks = galleryPage.GetImagesLinks();
                imagesLinks[0].Click();
                ImageViewer viewer = new ImageViewer(driver);
                wait.Until(ExpectedConditions.ElementIsVisible(viewer.imageViewBy));
                string firstImageLink = viewer.GetImageView().GetAttribute("src");
                int countV = viewer.GetImagesCount();
                if (countV != imagesLinks.Count)
                    Assert.Fail();
                for (int i = 1; i <= imagesLinks.Count; i++)
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(viewer.imageViewBy));
                    int current = viewer.GetCurrentImageNumber();
                    if (current != i)
                        Assert.Fail();
                    viewer.NextImage();
                }
                if(viewer.GetImageView().GetAttribute("src") != firstImageLink)
                    Assert.Fail();
            }
        }

        [Test]
        public void TstCheckImagesDownload()
        {
            //TEST SETUP//
            var driver = TestDataTask3.DataForTstCheckImagesDownload.Driver;
            driver.Url = TestDataTask3.DataForTstCheckImagesDownload.StartUrl;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //TEST
            //using (driver)
            //{
                ArticlePage articlePage = new ArticlePage(driver);
                articlePage.GoToImageGallery();
                ImageGalleryPage galleryPage = new ImageGalleryPage(driver);
                galleryPage.SelectAllImages();
                galleryPage.ExportSelectedToPowerPoint();
            //TODO file size check
            //}
        }

        [Test]
        public void TstAddArticleToFavorites()
        {
            //TEST SETUP//
            var driver = TestDataTask3.DataForTstAddArticleToFavorites.Driver;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var login = TestDataTask3.DataForTstAddArticleToFavorites.Login;
            var pass = TestDataTask3.DataForTstAddArticleToFavorites.Pass;

            //TEST
            //using (driver)
            //{
            driver.Url = "http://journals.lww.com/pages/default.aspx";
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login(login, pass);
            driver.Url = TestDataTask3.DataForTstAddArticleToFavorites.StartUrl;
            driver.ExecuteJavaScript("ArticleTools_ShowAddToMyCollectionsPopUp();");
            AddToFavoritesPopUp popUp = new AddToFavoritesPopUp(driver);
            popUp.AddToFolder("Test 2");
            popUp.CloseMessage();
            UserActionsToolBarPage toolBarPage = new UserActionsToolBarPage(driver);
            toolBarPage.GoToMyAccount();
            MyAccountPage accountPage = new MyAccountPage(driver);
            accountPage.GoToFavoritesTab();
            var articlesInFolder =  accountPage.GetFavoritesFromFolder("Test 2");
            System.Threading.Thread.Sleep(1500);
            Assert.IsTrue(articlesInFolder.Contains(TestDataTask3.DataForTstAddArticleToFavorites.StartUrl));
            accountPage.DeleteCurrentFolder();
            //}
        }
    }
}
