using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Enums;
using TestsLibrary.Models;
using TestsLibrary.Pages;
using TestsLibrary.SOLR;
using TestsLibrary.Utils;

namespace Tests.Task_2
{
    public class Tests_Task2 : SetUp_Task2
    {
        public Tests_Task2(string profile, string environment) : base(profile, environment) { }

        private static string StartUrl;
        private static QueryStringOptions QsOptions;
        private static FilterQueriesOptions FqOptions;
        private static string[] Products;
        private static string SRequest;


        //Articles by title with cme withing all dates
        [Test]
        public void TstAdvSearchCase1()
        {
            StartUrl = "http://journals.lww.com/ccmjournal/pages/advancedsearch.aspx";
            QsOptions = new QueryStringOptions(_title: "A");
            FqOptions = new FilterQueriesOptions(_articles: true, _cme: true,
                _pDate: PublicationDateEnum.AllDates, _sorting: SortByOptionsEnum.Newest);
            Products = new[] { "ccme" };
            SRequest = SolrRequest.GenerateRequest(QsOptions, FqOptions, Products);
            Driver.Url = StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(SRequest);
            //Titles and ids from api, exepting PAP
            var titlesApi = searchResponse.GetTitlesApiWithoutPap();
            var idsApi = searchResponse.GetArticleIdsApiWithoutPap();
            var countS = searchResponse.TotalFound;
            //Titles and ids from ui, exepting PAP
            List<string> titlesUi;
            List<string> idsUi;
            int countW;
            AdvSearchPage searchPage = new AdvSearchPage(Driver);
            searchPage.SelectSearchOptions(QsOptions, FqOptions, Products);
            searchPage.GoSearching();
            Wait.Until(ExpectedConditions.ElementToBeClickable(SearchResultPage.SortByDropDownListBy));
            SearchResultPage resultsPage = new SearchResultPage(Driver);
            resultsPage.SelectSortByOption(FqOptions.sorting);
            System.Threading.Thread.Sleep(4000);
            resultsPage.GetTitlesAndIds(out titlesUi, out idsUi);
            countW = resultsPage.GetResultCount();
            //Assertation
            Assert.AreEqual(countS, countW);
            bool a = Comparers.CompareTitles(titlesUi, titlesApi);
            bool b = Comparers.CompareIds(idsUi, idsApi);
            Assert.AreEqual(true, a);
            Assert.AreEqual(true, b);
        }

        //All key words aricles and images withing last five years/ best match
        [Test]
        public void TstAdvSearchCase2()
        {
            StartUrl = "http://journals.lww.com/aacr/pages/advancedsearch.aspx";
            QsOptions = new QueryStringOptions(_qAllKeys: "a");
            FqOptions = new FilterQueriesOptions(_articles: true,
                _image: true, _pDate: PublicationDateEnum.Last5Years, _sorting: SortByOptionsEnum.BestMatch);
            Products = new[] { "aacr" };
            SRequest = SolrRequest.GenerateRequest(QsOptions, FqOptions, Products);
            Driver.Url = StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(SRequest);
            //TEST
            //Results count from API
            int countS = searchResponse.TotalFound;
            //Results count from UI
            int countW;
            AdvSearchPage searchPage = new AdvSearchPage(Driver);
            searchPage.SelectSearchOptions(QsOptions, FqOptions, Products);
            searchPage.GoSearching();
            Wait.Until(ExpectedConditions.ElementToBeClickable(SearchResultPage.SortByDropDownListBy));
            SearchResultPage resultsPage = new SearchResultPage(Driver);
            countW = resultsPage.GetResultCount();
            //Assertions
            Assert.AreNotEqual(0, countS);
            Assert.AreNotEqual(0, countW);
            Assert.AreEqual(countS, countW);
        }

        //Images by all keys  withing all dates by oldest
        [Test]
        public void TstAdvSearchCase3()
        {
            StartUrl = "http://journals.lww.com/ccmjournal/pages/advancedsearch.aspx";
            QsOptions = new QueryStringOptions(_qAllKeys: "a");
            FqOptions = new FilterQueriesOptions(_image: true, _articles: false,
                _sorting: SortByOptionsEnum.Newest);
            Products = new[] { "ccme" };
            SRequest = SolrRequest.GenerateRequest(QsOptions, FqOptions, Products);
            Driver.Url = StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(SRequest);
            //TEST
            //Titles and ids from api, exepting PAP
            var titlesApi = searchResponse.GetTitlesApiWithoutPap();
            var idsApi = searchResponse.GetArticleIdsApiWithoutPap();
            var countS = searchResponse.TotalFound;
            //Titles and ids from ui, exepting PAP
            List<string> titlesUi;
            List<string> idsUi;
            int countW;
            AdvSearchPage searchPage = new AdvSearchPage(Driver);
            searchPage.SelectSearchOptions(QsOptions, FqOptions, Products);
            searchPage.GoSearching();
            Wait.Until(ExpectedConditions.ElementToBeClickable(SearchResultPage.SortByDropDownListBy));
            SearchResultPage resultsPage = new SearchResultPage(Driver);
            resultsPage.SelectSortByOption(FqOptions.sorting);
            System.Threading.Thread.Sleep(4000);
            resultsPage.GetTitlesAndIds(out titlesUi, out idsUi);
            countW = resultsPage.GetResultCount();
            //Assertation
            Assert.AreEqual(countS, countW);
            bool b = Comparers.CompareIds(idsUi, idsApi);
            Assert.AreEqual(true, b);
        }

       //Articles by all keys with open access withing all dates
        [Test]
        public void TstAdvSearchCase4()
        {
            StartUrl = "http://journals.lww.com/ccmjournal/pages/advancedsearch.aspx";
            QsOptions = new QueryStringOptions(_qAllKeys: "A");
            FqOptions = new FilterQueriesOptions(_articles: true, _openAccess: true,
                _pDate: PublicationDateEnum.AllDates, _sorting: SortByOptionsEnum.Newest);
            Products = new[] { "ccme" };
            SRequest = SolrRequest.GenerateRequest(QsOptions, FqOptions, Products);
            Driver.Url = StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(SRequest);
            //Titles and ids from api, exepting PAP
            var titlesApi = searchResponse.GetTitlesApiWithoutPap();
            var idsApi = searchResponse.GetArticleIdsApiWithoutPap();
            var countS = searchResponse.TotalFound;
            //Titles and ids from ui, exepting PAP
            List<string> titlesUi;
            List<string> idsUi;
            int countW;
            AdvSearchPage searchPage = new AdvSearchPage(Driver);
            searchPage.SelectSearchOptions(QsOptions, FqOptions, Products);
            searchPage.GoSearching();
            Wait.Until(ExpectedConditions.ElementToBeClickable(SearchResultPage.SortByDropDownListBy));
            SearchResultPage resultsPage = new SearchResultPage(Driver);
            resultsPage.SelectSortByOption(FqOptions.sorting);
            System.Threading.Thread.Sleep(4000);
            resultsPage.GetTitlesAndIds(out titlesUi, out idsUi);
            countW = resultsPage.GetResultCount();
            //Assertation
            Assert.AreEqual(countS, countW);
            bool a = Comparers.CompareTitles(titlesUi, titlesApi);
            bool b = Comparers.CompareIds(idsUi, idsApi);
            Assert.AreEqual(true, a);
            Assert.AreEqual(true, b);
        }
    }
}
