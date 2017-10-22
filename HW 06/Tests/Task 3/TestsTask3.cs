using System;
using NUnit.Framework;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Pages;
using TestsLibrary.Utils;

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
            //using (driver)
            //{
                ArticlePage articlePage = new ArticlePage(driver);
                articlePage.GoToImageGallery();
                if (DriverUtils.isAlertPresent(driver))
                    driver.SwitchTo().Alert().Accept();
                wait.Until(ExpectedConditions.ElementIsVisible(ImageGalleryPage.actionsDropDownListToggleBy));
                ImageGalleryPage galleryPage = new ImageGalleryPage(driver);
                var imagesLinks = galleryPage.GetImagesLinks();
                imagesLinks[0].Click();
                wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.imageViewBy));
                bool a = true;
                Assert.IsTrue(a);
            //}
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
                if (DriverUtils.isAlertPresent(driver))
                    driver.SwitchTo().Alert().Accept();
                wait.Until(ExpectedConditions.ElementIsVisible(ImageGalleryPage.actionsDropDownListToggleBy));
                ImageGalleryPage galleryPage = new ImageGalleryPage(driver);
                var imagesLinks = galleryPage.GetImagesLinks();
                imagesLinks[0].Click();
                wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.imageViewBy));
                ImageViewer viewer = new ImageViewer(driver);
                string firstImageLink = viewer.GetImageView().GetAttribute("src");
                int countV = viewer.GetImagesCount();
                if (countV != imagesLinks.Count)
                    Assert.Fail();
                for (int i = 1; i <= imagesLinks.Count; i++)
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(ImageViewer.imageViewBy));
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
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(ArticlePage.viewImagesGalleryBy));
                ArticlePage articlePage = new ArticlePage(driver);
                articlePage.GoToImageGallery();
                if (DriverUtils.isAlertPresent(driver))
                driver.SwitchTo().Alert().Accept();
                wait.Until(ExpectedConditions.ElementIsVisible(ImageGalleryPage.actionsDropDownListToggleBy));
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
            var folderName = TestDataTask3.DataForTstAddArticleToFavorites.FolderName;

            //TEST
            using (driver)
            {
                driver.Url = "http://journals.lww.com/pages/default.aspx";
                wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.SubmitButtonBy));
                LoginPage loginPage = new LoginPage(driver);
                loginPage.Login(login, pass);
                wait.Until(ExpectedConditions.ElementIsVisible(UserActionsToolBarPage.logOutButtonBy));
                UserActionsToolBarPage toolBarPage = new UserActionsToolBarPage(driver);
                driver.Url = TestDataTask3.DataForTstAddArticleToFavorites.StartUrl;
                if (DriverUtils.isAlertPresent(driver))
                    driver.SwitchTo().Alert().Accept();
                System.Threading.Thread.Sleep(4000); //Bad wait
                //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(ArticlePage.viewImagesGalleryBy));
                driver.ExecuteJavaScript("ArticleTools_ShowAddToMyCollectionsPopUp();");
                wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.existingFoldersListBy));
                AddToFavoritesPopUp popUp = new AddToFavoritesPopUp(driver);
                popUp.AddToNewFolder(folderName);
                wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.messageCancelButtonBy));
                popUp.CloseMessage();
                toolBarPage.GoToMyAccount();
                MyAccountPage accountPage = new MyAccountPage(driver);
                wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.myFavoritesTabBy));
                accountPage.GoToFavoritesTab();
                var articlesInFolder =  accountPage.GetFavoritesLinksFromFolder(folderName);
                if (DriverUtils.isAlertPresent(driver))
                    driver.SwitchTo().Alert().Accept();
                System.Threading.Thread.Sleep(4000); //Bad wait
                Assert.IsTrue(articlesInFolder.Contains(TestDataTask3.DataForTstAddArticleToFavorites.StartUrl));
                accountPage.DeleteCurrentFolder();
                wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.deleteCollectionConfirmButtonBy));
                accountPage.DeleteCurrentFolderConfirm();
            }
        }

        [Test]
        public void TstAddArticleToFavoritesFromIssue()
        {
            //TEST SETUP//
            var driver = TestDataTask3.DataForTstAddArticleToFavoritesFromIssue.Driver;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var login = TestDataTask3.DataForTstAddArticleToFavoritesFromIssue.Login;
            var pass = TestDataTask3.DataForTstAddArticleToFavoritesFromIssue.Pass;
            var folderName = TestDataTask3.DataForTstAddArticleToFavoritesFromIssue.FolderName;

            //TEST
            using (driver)
            {
                driver.Url = "http://journals.lww.com/pages/default.aspx";
                wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.SubmitButtonBy));
                LoginPage loginPage = new LoginPage(driver);
                loginPage.Login(login, pass);
                wait.Until(ExpectedConditions.ElementIsVisible(UserActionsToolBarPage.logOutButtonBy));
                UserActionsToolBarPage toolBarPage = new UserActionsToolBarPage(driver);
                driver.Url = TestDataTask3.DataForTstAddArticleToFavoritesFromIssue.StartUrl;
                if (DriverUtils.isAlertPresent(driver))
                    driver.SwitchTo().Alert().Accept();
                wait.Until(ExpectedConditions.ElementIsVisible(CurrentIssuePage.subscribeTOCBy));
                CurrentIssuePage currentIssuePage = new CurrentIssuePage(driver);
                var articlesList = currentIssuePage.ArticlesContainer.GetArticlesList();
                currentIssuePage.ArticlesContainer.AddArticleToFavorites(articlesList[1]);

                wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.existingFoldersListBy));
                AddToFavoritesPopUp popUp = new AddToFavoritesPopUp(driver);
                popUp.AddToFolder(folderName);
                wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.messageCancelButtonBy));
                popUp.CloseMessage();
                toolBarPage.GoToMyAccount();
                MyAccountPage accountPage = new MyAccountPage(driver);
                wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.myFavoritesTabBy));
                accountPage.GoToFavoritesTab();
                var articlesInFolder = accountPage.GetFavoritesLinksFromFolder(folderName);
                if (DriverUtils.isAlertPresent(driver))
                    driver.SwitchTo().Alert().Accept();
                System.Threading.Thread.Sleep(2000); //Bad wait
                Assert.IsTrue(articlesInFolder.Contains(articlesList[1].Href));
                accountPage.DeleteCurrentFolder();
                wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.deleteCollectionConfirmButtonBy));
                accountPage.DeleteCurrentFolderConfirm();
            }
        }
    }
}
