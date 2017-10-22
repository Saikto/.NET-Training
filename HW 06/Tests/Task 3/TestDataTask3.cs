using OpenQA.Selenium;
using TestsLibrary;
using TestsLibrary.Enums;

namespace Tests.Task_3
{
    class TestDataTask3
    {
        public static class DataForTstOpenFristImage
        {
            public static string MethodName = "TstOpenFristImage";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
            public static string StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
        }

        public static class DataForTstCheckImagesIteration
        {
            public static string MethodName = "TstCheckImagesIteration";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
            public static string StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
        }

        public static class DataForTstCheckImagesDownload
        {
            public static string MethodName = "TstCheckImagesDownload";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
            public static string StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
        }

        public static class DataForTstAddArticleToFavorites
        {
            public static string MethodName = "TstAddArticleToFavorites";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
            public static string StartUrl = "http://journals.lww.com/ccmjournal/Fulltext/2017/11000/Investigating_the_Impact_of_Different_Suspicion_of.2.aspx";
            public static string Login = "igor_neslukhovski@epam.com";
            public static string Pass = "epam_test1";
            public static string FolderName = "Test 2";
        }

        public static class DataForTstAddArticleToFavoritesFromIssue
        {
            public static string MethodName = "TstAddArticleToFavoritesFromIssue";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
            public static string StartUrl = "http://journals.lww.com/ccmjournal/pages/currenttoc.aspx";
            public static string Login = "igor_neslukhovski@epam.com";
            public static string Pass = "epam_test1";
            public static string FolderName = "Test 2";
        }
    }
}
