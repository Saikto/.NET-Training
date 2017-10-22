using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Pages;
using TestsLibrary.SOLR;
using TestsLibrary.Utils;

namespace Tests.Task_2
{
    public class TestsTask2
    {
        //Articles by title with cme withing all dates
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstAdvSearchCase1()
        {
            //TEST SETUP//
            var driver = TestDataTask2.DataForTstAdvSearchCase1.Driver;
            var qsOptions = TestDataTask2.DataForTstAdvSearchCase1.QsOptions;
            var fqOptions = TestDataTask2.DataForTstAdvSearchCase1.FqOptions;
            var products = TestDataTask2.DataForTstAdvSearchCase1.Products;
            var sRequest = TestDataTask2.DataForTstAdvSearchCase1.SRequest;
            driver.Url = TestDataTask2.DataForTstAdvSearchCase1.StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(sRequest);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //TEST
            //Titles and ids from api, exepting PAP
            var titlesApi = searchResponse.GetTitlesApiWithoutPap();
            var idsApi = searchResponse.GetArticleIdsApiWithoutPap();
            var countS = searchResponse.TotalFound;
            //Titles and ids from ui, exepting PAP
            List<string> titlesUi;
            List<string> idsUi;
            int countW;
            using (driver)
            {
                AdvSearchPage searchPage = new AdvSearchPage(driver);
                searchPage.SelectSearchOptions(qsOptions, fqOptions, products);
                searchPage.GoSearching();
                wait.Until(ExpectedConditions.ElementToBeClickable(SearchResultPage.SortByDropDownListBy));
                SearchResultPage resultsPage = new SearchResultPage(driver);
                resultsPage.SelectSortByOption(fqOptions.sorting);
                System.Threading.Thread.Sleep(4000);
                resultsPage.GetTitlesAndIds(out titlesUi, out idsUi);
                countW = resultsPage.GetResultCount();
            }
            //Assertation
            Assert.AreEqual(countS, countW);
            bool a = Comparers.CompareTitles(titlesUi, titlesApi);
            bool b = Comparers.CompareIds(idsUi, idsApi);
            Assert.AreEqual(true, a);
            Assert.AreEqual(true, b);
        }

        //All key words aricles and images withing last five years/ best match
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstAdvSearchCase2()
        {
            //TEST SETUP//
            var driver = TestDataTask2.DataForTstAdvSearchCase2.Driver;
            var qsOptions = TestDataTask2.DataForTstAdvSearchCase2.QsOptions;
            var fqOptions = TestDataTask2.DataForTstAdvSearchCase2.FqOptions;
            var products = TestDataTask2.DataForTstAdvSearchCase2.Products;
            var sRequest = TestDataTask2.DataForTstAdvSearchCase2.SRequest;
            driver.Url = TestDataTask2.DataForTstAdvSearchCase2.StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(sRequest);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //TEST
            //Results count from API
            int countS = searchResponse.TotalFound;
            //Results count from UI
            int countW;
            using (driver)
            {
                AdvSearchPage searchPage = new AdvSearchPage(driver);
                searchPage.SelectSearchOptions(qsOptions, fqOptions, products);
                searchPage.GoSearching();
                wait.Until(ExpectedConditions.ElementToBeClickable(SearchResultPage.SortByDropDownListBy));
                SearchResultPage resultsPage = new SearchResultPage(driver);
                countW = resultsPage.GetResultCount();
            }
            //Assertions
            Assert.AreNotEqual(0, countS);
            Assert.AreNotEqual(0, countW);
            Assert.AreEqual(countS, countW);
        }

        //Images by all keys  withing all dates by oldest
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstAdvSearchCase3()
        {
            //TEST SETUP//
            var driver = TestDataTask2.DataForTstAdvSearchCase3.Driver;
            var qsOptions = TestDataTask2.DataForTstAdvSearchCase3.QsOptions;
            var fqOptions = TestDataTask2.DataForTstAdvSearchCase3.FqOptions;
            var products = TestDataTask2.DataForTstAdvSearchCase3.Products;
            var sRequest = TestDataTask2.DataForTstAdvSearchCase3.SRequest;
            driver.Url = TestDataTask2.DataForTstAdvSearchCase3.StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(sRequest);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //TEST
            //Titles and ids from api, exepting PAP
            var titlesApi = searchResponse.GetTitlesApiWithoutPap();
            var idsApi = searchResponse.GetArticleIdsApiWithoutPap();
            var countS = searchResponse.TotalFound;
            //Titles and ids from ui, exepting PAP
            List<string> titlesUi;
            List<string> idsUi;
            int countW;
            using (driver)
            {
                AdvSearchPage searchPage = new AdvSearchPage(driver);
                searchPage.SelectSearchOptions(qsOptions, fqOptions, products);
                searchPage.GoSearching();
                wait.Until(ExpectedConditions.ElementToBeClickable(SearchResultPage.SortByDropDownListBy));
                SearchResultPage resultsPage = new SearchResultPage(driver);
                resultsPage.SelectSortByOption(fqOptions.sorting);
                System.Threading.Thread.Sleep(4000);
                resultsPage.GetTitlesAndIds(out titlesUi, out idsUi);
                countW = resultsPage.GetResultCount();
            }
            //Assertation
            Assert.AreEqual(countS, countW);
            //bool a = Comparers.CompareTitles(titlesUi, titlesApi);
            bool b = Comparers.CompareIds(idsUi, idsApi);
            //Assert.AreEqual(true, a);
            Assert.AreEqual(true, b);
        }

        //Articles by all keys with open access withing all dates
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstAdvSearchCase4()
        {
            //TEST SETUP//
            var driver = TestDataTask2.DataForTstAdvSearchCase4.Driver;
            var qsOptions = TestDataTask2.DataForTstAdvSearchCase4.QsOptions;
            var fqOptions = TestDataTask2.DataForTstAdvSearchCase4.FqOptions;
            var products = TestDataTask2.DataForTstAdvSearchCase4.Products;
            var sRequest = TestDataTask2.DataForTstAdvSearchCase4.SRequest;
            driver.Url = TestDataTask2.DataForTstAdvSearchCase4.StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(sRequest);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //TEST
            //Titles and ids from api, exepting PAP
            var titlesApi = searchResponse.GetTitlesApiWithoutPap();
            var idsApi = searchResponse.GetArticleIdsApiWithoutPap();
            var countS = searchResponse.TotalFound;
            //Titles and ids from ui, exepting PAP
            List<string> titlesUi;
            List<string> idsUi;
            int countW;
            using (driver)
            {
                AdvSearchPage searchPage = new AdvSearchPage(driver);
                searchPage.SelectSearchOptions(qsOptions, fqOptions, products);
                searchPage.GoSearching();
                wait.Until(ExpectedConditions.ElementToBeClickable(SearchResultPage.SortByDropDownListBy));
                SearchResultPage resultsPage = new SearchResultPage(driver);
                resultsPage.SelectSortByOption(fqOptions.sorting);
                System.Threading.Thread.Sleep(4000);
                resultsPage.GetTitlesAndIds(out titlesUi, out idsUi);
                countW = resultsPage.GetResultCount();
            }
            //Assertation
            Assert.AreEqual(countS, countW);
            bool a = Comparers.CompareTitles(titlesUi, titlesApi);
            bool b = Comparers.CompareIds(idsUi, idsApi);
            Assert.AreEqual(true, a);
            Assert.AreEqual(true, b);
        }
    }
}
