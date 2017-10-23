using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsLibrary;
using TestsLibrary.Enums;
using TestsLibrary.Pages;

namespace BrowserStack.Task_3
{
    [SetUpFixture]
    public class TestDataTask3
    {
        private static string FixtureName = "Task3";
        private static string Browser;
        public static IWebDriver Driver;
        public static WebDriverWait Wait;
        public static string Login;
        public static string Pass;

        [OneTimeSetUp]
        public void TestsTask3Init()
        {
            Browser = Browsers.Chrome.ToString();
            Driver = WebDriverSelector.GetWebDriver(FixtureName, Browser);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
            Login = "igor_neslukhovski@epam.com";
            Pass = "epam_test1";
            Driver.Url = "http://journals.lww.com/pages/default.aspx";
            Wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.SubmitButtonBy));
            LoginPage loginPage = new LoginPage(Driver);
            loginPage.Login(Login, Pass);
            Wait.Until(ExpectedConditions.ElementIsVisible(UserActionsToolBarPage.logOutButtonBy));
        }

        [OneTimeTearDown]
        public void TestsTask3Dispose()
        {
            Driver.Dispose();
        }
        public static class DataForTstOpenFristImage
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
        }

        public static class DataForTstCheckImagesIteration
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
        }

        public static class DataForTstCheckImagesDownload
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
            public static int ExpectedFileSize = 948;
            public static string FilePath = @"C:\Users\igor_\Downloads\image_download.pptx";
            //public static string FilePath = @"C:\Users\igor_\Downloads\image_download.pptx";
        }

        public static class DataForTstAddArticleToFavorites
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
            public static string FolderName = "Test 2";
        }

        public static class DataForTstAddArticleToFavoritesFromIssue
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/pages/currenttoc.aspx";
            public static string FolderName = "Test 2";
        }
    }
}
