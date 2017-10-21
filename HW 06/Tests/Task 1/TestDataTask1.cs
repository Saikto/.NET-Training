using OpenQA.Selenium;
using TestsLibrary;
using TestsLibrary.Enums;

namespace Tests.Task_1
{
    public static class TestDataTask1
    {
        public static class DataForTstCurrentIssueLinksExist
        {
            public static string MethodName = "TstCurrentIssueLinksExist";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
            public static string StartUrl = "http://journals.lww.com/asaiojournal";
        }

        public static class DataForTstMenuElementsExist
        {
            public static string MethodName = "TstMenuElementsExist";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
            public static string StartUrl = "http://journals.lww.com/asaiojournal"; //"http://journals.lww.com/annalsplasticsurgery" //Only FREE article
        }

        public static class DataForTstLogIn
        {
            public static string MethodName = "TstLogIn";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
            public static string StartUrl = "http://journals.lww.com";
            public static string Login = "igor_neslukhovski@epam.com";
            public static string Pass = "epam_test1";
        }
    }
}
