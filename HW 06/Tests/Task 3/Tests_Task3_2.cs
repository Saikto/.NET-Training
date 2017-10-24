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
    public class Tests_Task3_2 : SetUp_Task3
    {
        public Tests_Task3_2(string profile, string environment) : base(profile, environment) { }

        private static string StartUrl;
        private static string Login;
        private static string Pass;
        private static int ExpectedFileSize;
        private static string FilePath;
        private static string FolderName;

        [SetUp]
        public void SetUp()
        {
            
            Login = "igor_neslukhovski@epam.com";
            Pass = "epam_test1";
            Driver.Url = "http://journals.lww.com/pages/default.aspx";
            Wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.SubmitButtonBy));
            LoginPage loginPage = new LoginPage(Driver);
            loginPage.Login(Login, Pass);
            Wait.Until(ExpectedConditions.ElementIsVisible(UserActionsToolBarPage.LogOutButtonBy));
        }

        [TearDown]
        public void TstAddArticleToFavoritesFromIssueTearDown()
        {
            MyAccountPage accountPage = new MyAccountPage(Driver);
            accountPage.DeleteCurrentFolder();
            Wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.DeleteCollectionConfirmButtonBy));
            accountPage.DeleteCurrentFolderConfirm();
            UserActionsToolBarPage toolBarPage = new UserActionsToolBarPage(Driver);
            toolBarPage.LogOut();
        }

        [Test]
        public void TstAddArticleToFavorites()
        {
            StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
            FolderName = "Test 2";
            Driver.Url = StartUrl;
            UserActionsToolBarPage toolBarPage = new UserActionsToolBarPage(Driver);
            Driver.ExecuteJavaScript("ArticleTools_ShowAddToMyCollectionsPopUp();");
            Wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.ExistingFoldersListBy));
            AddToFavoritesPopUp popUp = new AddToFavoritesPopUp(Driver);
            //popUp.AddToNewFolder(folderName);
            popUp.AddToFavoritesAnyway(FolderName);
            Wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.MessageCancelButtonBy));
            popUp.CloseMessage();
            toolBarPage.GoToMyAccount();
            MyAccountPage accountPage = new MyAccountPage(Driver);
            Wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.MyFavoritesTabBy));
            accountPage.GoToFavoritesTab();
            var articlesInFolder = accountPage.GetFavoritesLinksFromFolder(FolderName);
            Assert.IsTrue(articlesInFolder.Contains(StartUrl));
        }

        [Test]
        public void TstAddArticleToFavoritesFromIssue()
        {
            StartUrl = "http://journals.lww.com/ccmjournal/pages/currenttoc.aspx";
            FolderName = "Test 2";
            Driver.Url = StartUrl;
            UserActionsToolBarPage toolBarPage = new UserActionsToolBarPage(Driver);
            Wait.Until(ExpectedConditions.ElementIsVisible(CurrentIssuePage.SubscribeTocBy));
            CurrentIssuePage currentIssuePage = new CurrentIssuePage(Driver);
            var articlesList = currentIssuePage.ArticlesContainer.GetArticlesList();
            currentIssuePage.ArticlesContainer.AddArticleToFavorites(articlesList[1]);
            Wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.ExistingFoldersListBy));
            AddToFavoritesPopUp popUp = new AddToFavoritesPopUp(Driver);
            //popUp.AddToFolder(folderName);
            popUp.AddToFavoritesAnyway(FolderName);
            Wait.Until(ExpectedConditions.ElementIsVisible(AddToFavoritesPopUp.MessageCancelButtonBy));
            popUp.CloseMessage();
            toolBarPage.GoToMyAccount();
            MyAccountPage accountPage = new MyAccountPage(Driver);
            Wait.Until(ExpectedConditions.ElementIsVisible(MyAccountPage.MyFavoritesTabBy));
            accountPage.GoToFavoritesTab();
            var articlesInFolder = accountPage.GetFavoritesLinksFromFolder(FolderName);
            Assert.IsTrue(articlesInFolder.Contains(articlesList[1].Href));
        }
    }
}
