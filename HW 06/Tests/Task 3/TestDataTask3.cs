using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsLibrary;
using TestsLibrary.Enums;

namespace Tests.Task_3
{
    [SetUpFixture]
    public class TestDataTask3
    {
        private static string FixtureName = "Task3";
        private static string Browser;
        public static IWebDriver Driver;
        public static WebDriverWait Wait;

        [OneTimeSetUp]
        public void TestsTask3Init()
        {
            Browser = Browsers.Chrome.ToString();
            Driver = WebDriverSelector.GetWebDriver(FixtureName, Browser);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
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
        }

        public static class DataForTstAddArticleToFavorites
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
            public static string Login = "igor_neslukhovski@epam.com";
            public static string Pass = "epam_test1";
            public static string FolderName = "Test 2";
        }

        public static class DataForTstAddArticleToFavoritesFromIssue
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/pages/currenttoc.aspx";
            public static string Login = "igor_neslukhovski@epam.com";
            public static string Pass = "epam_test1";
            public static string FolderName = "Test 2";
        }
    }
}
