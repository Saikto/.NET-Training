using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsLibrary;
using TestsLibrary.Enums;
using TestsLibrary.Models;
using TestsLibrary.SOLR;

namespace BrowserStack.Task_2
{
    [SetUpFixture]
    public class TestDataTask2
    {
        private static string FixtureName = "Task2";
        private static string Browser;
        public static IWebDriver Driver;
        public static WebDriverWait Wait;

        [OneTimeSetUp]
        public void TestsTask2Init()
        {
            Browser = Browsers.FireFox.ToString();
            Driver = WebDriverSelector.GetWebDriver(FixtureName, Browser);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [OneTimeTearDown]
        public void TestsTask2Dispose()
        {
            Driver.Dispose();
        }

        public static class DataForTstAdvSearchCase1
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/pages/advancedsearch.aspx";
            //Search options setup
            public static QueryStringOptions QsOptions = new QueryStringOptions(_title: "A");
            public static FilterQueriesOptions FqOptions = new FilterQueriesOptions(_articles: true, _cme: true,
                _pDate: PublicationDateEnum.AllDates, _sorting: SortByOptionsEnum.Newest);
            public static string[] Products = { "ccme" };
            public static string SRequest = SolrRequest.GenerateRequest(QsOptions, FqOptions, Products);
        }

        public static class DataForTstAdvSearchCase2
        {
            public static string StartUrl = "http://journals.lww.com/aacr/pages/advancedsearch.aspx";
            //Search options setup
            public static QueryStringOptions QsOptions = new QueryStringOptions(_qAllKeys: "a");
            public static FilterQueriesOptions FqOptions = new FilterQueriesOptions(_articles: true,
                _image: true, _pDate: PublicationDateEnum.Last5Years, _sorting: SortByOptionsEnum.BestMatch);
            public static string[] Products = { "aacr" };
            public static string SRequest = SolrRequest.GenerateRequest(QsOptions, FqOptions, Products);
        }

        public static class DataForTstAdvSearchCase3
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/pages/advancedsearch.aspx";
            //Search options setup
            public static QueryStringOptions QsOptions = new QueryStringOptions(_qAllKeys: "a");
            public static FilterQueriesOptions FqOptions = new FilterQueriesOptions(_image:true, _articles:false,
                    _sorting: SortByOptionsEnum.Newest);
            public static string[] Products = { "ccme" };
            public static string SRequest = SolrRequest.GenerateRequest(QsOptions, FqOptions, Products);
        }

        public static class DataForTstAdvSearchCase4
        {
            public static string StartUrl = "http://journals.lww.com/ccmjournal/pages/advancedsearch.aspx";
            //Search options setup
            public static QueryStringOptions QsOptions = new QueryStringOptions(_qAllKeys: "A");
            public static FilterQueriesOptions FqOptions = new FilterQueriesOptions(_articles: true, _openAccess: true,
                _pDate: PublicationDateEnum.AllDates, _sorting: SortByOptionsEnum.Newest);
            public static string[] Products = { "ccme" };
            public static string SRequest = SolrRequest.GenerateRequest(QsOptions, FqOptions, Products);
        }
    }
}
