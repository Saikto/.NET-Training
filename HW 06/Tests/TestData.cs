using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TestsLibrary;
using TestsLibrary.Enums;
using TestsLibrary.Models;
using TestsLibrary.SOLR;

namespace Tests
{
    public static class TestData
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
