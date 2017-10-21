using OpenQA.Selenium;
using TestsLibrary;
using TestsLibrary.Enums;
using TestsLibrary.Models;
using TestsLibrary.SOLR;

namespace Tests.Task_2
{
    public static class TestDataTask2
    {
        public static class DataForTstAdvSearchCase1
        {
            public static string MethodName = "TstAdvSearhCase1";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
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
            public static string MethodName = "TstAdvSearchCase2";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
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
            public static string MethodName = "TstAdvSearchCase3";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
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
            public static string MethodName = "TstAdvSearchCase4";
            public static string Browser = Browsers.FireFox.ToString();
            public static IWebDriver Driver = WebDriverSelector.GetWebDriver(MethodName, Browser);
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
