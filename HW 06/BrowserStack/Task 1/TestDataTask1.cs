using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsLibrary;
using TestsLibrary.Enums;

namespace BrowserStack.Task_1
{
    [SetUpFixture]
    public class TestDataTask1
    {
        private static string FixtureName = "Task1";
        private static string Browser;
        public static IWebDriver Driver;
        public static WebDriverWait Wait;

        [OneTimeSetUp]
        public void TestsTask1Init()
        {
            Browser = Browsers.FireFox.ToString();
            Driver = WebDriverSelector.GetWebDriver(FixtureName, Browser);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void TestsTask1Dispose()
        {
            Driver.Dispose();
        }

        public static class DataForTstCurrentIssueLinksExist
        {
            public static string StartUrl = "http://journals.lww.com/asaiojournal";
        }

        public static class DataForTstMenuElementsExist
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/";
            //public static string StartUrl = "http://journals.lww.com/annalsplasticsurgery/"; //Only FREE article
        }

        public static class DataForTstLogIn
        {
            public static string StartUrl = "http://journals.lww.com";
            public static string Login = "igor_neslukhovski@epam.com";
            public static string Pass = "epam_test1";
        }
    }
}
