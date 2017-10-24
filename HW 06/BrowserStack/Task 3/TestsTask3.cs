using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Pages;
using TestsLibrary.Utils;

namespace BrowserStack.Task_3
{
    
    [TestFixture("single", "Win7FireFox")]
    [TestFixture("single", "Win10Chrome")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class TestsTask3 : BrowserStackNUnitTest
    {
        public TestsTask3(string profile, string environment) : base(profile, environment) { }

        //private static IWebDriver driver = TestDataTask3.Driver;
        private static WebDriverWait wait = TestDataTask3.Wait;

        [Test]
        public void TstOpenFristImageBS()
        {
            //TEST SETUP//
            driver.Url = TestDataTask3.DataForTstOpenFristImage.StartUrl;
            //TEST
            ArticlePage articlePage = new ArticlePage(driver);
            articlePage.GoToImageGallery();
            wait.Until(ExpectedConditions.ElementIsVisible(ImageGalleryPage.ActionsDropDownListToggleBy));
            ImageGalleryPage galleryPage = new ImageGalleryPage(driver);
            var imagesLinks = galleryPage.GetImagesLinks();
            imagesLinks[0].Click();
            wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.ImageViewBy));
            bool a = true;
            Assert.IsTrue(a);
        }

        [Test]
        public void TstCheckImagesIterationBS()
        {
            //TEST SETUP//
            driver.Url = TestDataTask3.DataForTstCheckImagesIteration.StartUrl;
            //TEST
            ArticlePage articlePage = new ArticlePage(driver);
            articlePage.GoToImageGallery();
            if (DriverUtils.isAlertPresent(driver))
                driver.SwitchTo().Alert().Accept();
            wait.Until(ExpectedConditions.ElementIsVisible(ImageGalleryPage.ActionsDropDownListToggleBy));
            ImageGalleryPage galleryPage = new ImageGalleryPage(driver);
            var imagesLinks = galleryPage.GetImagesLinks();
            imagesLinks[0].Click();
            wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.ImageViewBy));
            ImageViewer viewer = new ImageViewer(driver);
            string firstImageLink = viewer.GetImageView().GetAttribute("src");
            int countV = viewer.GetImagesCount();
            if (countV != imagesLinks.Count)
                Assert.Fail();
            for (int i = 1; i <= imagesLinks.Count; i++)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.ImageViewBy));
                int current = viewer.GetCurrentImageNumber();
                if (current != i)
                    Assert.Fail();
                viewer.NextImage();
            }
            wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.ImageViewBy));
            if(viewer.GetImageView().GetAttribute("src") != firstImageLink)
                Assert.Fail();
        
        }

        [Test]
        public void TstCheckImagesDownloadBS()
        {
            //TEST SETUP//
            driver.Url = TestDataTask3.DataForTstCheckImagesDownload.StartUrl;
            string filePath = TestDataTask3.DataForTstCheckImagesDownload.FilePath;
            int expectedSize = TestDataTask3.DataForTstCheckImagesDownload.ExpectedFileSize;
            //TEST
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(ArticlePage.ViewImagesGalleryBy));
            ArticlePage articlePage = new ArticlePage(driver);
            articlePage.GoToImageGallery();
            wait.Until(ExpectedConditions.ElementIsVisible(ImageGalleryPage.ActionsDropDownListToggleBy));
            ImageGalleryPage galleryPage = new ImageGalleryPage(driver);
            galleryPage.SelectAllImages();
            galleryPage.ExportSelectedToPowerPoint();
            System.Threading.Thread.Sleep(5000);
            int size = FileSize.GetFileSizeInKB(filePath);
            Assert.AreEqual(expectedSize, size, 1);
        }

        [Test]
        public void TstAddArticleToFavoritesBS()
        {
            //TEST SETUP//
            var folderName = TestDataTask3.DataForTstAddArticleToFavorites.FolderName;
            //TEST
            UserActionsToolBarPage toolBarPage = new UserActionsToolBarPage(driver);
            driver.Url = TestDataTask3.DataForTstAddArticleToFavorites.StartUrl;
            driver.ExecuteJavaScript("ArticleTools_ShowAddToMyCollectionsPopUp();");
            wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.ExistingFoldersListBy));
            AddToFavoritesPopUp popUp = new AddToFavoritesPopUp(driver);
            //popUp.AddToNewFolder(folderName);
            popUp.AddToFavoritesAnyway(folderName);
            wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.MessageCancelButtonBy));
            popUp.CloseMessage();
            toolBarPage.GoToMyAccount();
            MyAccountPage accountPage = new MyAccountPage(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.MyFavoritesTabBy));
            accountPage.GoToFavoritesTab();
            var articlesInFolder =  accountPage.GetFavoritesLinksFromFolder(folderName);
            Assert.IsTrue(articlesInFolder.Contains(TestDataTask3.DataForTstAddArticleToFavorites.StartUrl));
            accountPage.DeleteCurrentFolder();
            wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.DeleteCollectionConfirmButtonBy));
            accountPage.DeleteCurrentFolderConfirm();
            
        }

        [Test]
        public void TstAddArticleToFavoritesFromIssueBS()
        {
            //TEST SETUP//
            var folderName = TestDataTask3.DataForTstAddArticleToFavoritesFromIssue.FolderName;
            //TEST
            UserActionsToolBarPage toolBarPage = new UserActionsToolBarPage(driver);
            driver.Url = TestDataTask3.DataForTstAddArticleToFavoritesFromIssue.StartUrl;
            wait.Until(ExpectedConditions.ElementIsVisible(CurrentIssuePage.SubscribeTocBy));
            CurrentIssuePage currentIssuePage = new CurrentIssuePage(driver);
            var articlesList = currentIssuePage.ArticlesContainer.GetArticlesList();
            currentIssuePage.ArticlesContainer.AddArticleToFavorites(articlesList[1]);
            wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.ExistingFoldersListBy));
            AddToFavoritesPopUp popUp = new AddToFavoritesPopUp(driver);
            //popUp.AddToFolder(folderName);
            popUp.AddToFavoritesAnyway(folderName);
            wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.MessageCancelButtonBy));
            popUp.CloseMessage();
            toolBarPage.GoToMyAccount();
            MyAccountPage accountPage = new MyAccountPage(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.MyFavoritesTabBy));
            accountPage.GoToFavoritesTab();
            var articlesInFolder = accountPage.GetFavoritesLinksFromFolder(folderName);
            Assert.IsTrue(articlesInFolder.Contains(articlesList[1].Href));
            accountPage.DeleteCurrentFolder();
            wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.DeleteCollectionConfirmButtonBy));
            accountPage.DeleteCurrentFolderConfirm();
        }
    }
}
