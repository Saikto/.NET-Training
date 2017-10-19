using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using TestsLibrary;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Enums;
using TestsLibrary.Models;
using TestsLibrary.Pages;
using TestsLibrary.SOLR;
using TestsLibrary.Utils;

namespace Tests
{
    public class Tests
    {

        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstLogIn()
        {
            //TEST SETUP//
            var login = TestData.DataForTstLogIn.Login;
            var pass = TestData.DataForTstLogIn.Pass;
            var driver = TestData.DataForTstLogIn.Driver;
            driver.Url = TestData.DataForTstLogIn.StartUrl;

            //TEST
            using (driver)
            {
                LoginPage loginPage = new LoginPage(driver);
                loginPage.UserNameField.SendKeys(login);
                loginPage.PasswordField.SendKeys(pass);
                loginPage.SubmitButton.Click();
                driver.FindElement(By.Id("ctl00_ucUserActionsToolbar_lnkLogout"));
            }
        }

        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstMenuElementsExist()
        {
            //TEST SETUP//
            var driver = TestData.DataForTstCurrentIssueLinksExist.Driver;
            driver.Url = TestData.DataForTstCurrentIssueLinksExist.StartUrl;

            //TEST
            using (driver)
            {
                JournalPage journalPage = new JournalPage(driver);
                journalPage.FindOpenArticle().Click();
                //journalPage.FindFreeArticle().Click();
                ArticlePage articlePage = new ArticlePage(driver);
                List<string> menuAtions = articlePage.GetArticleMenu();

                Assert.IsTrue(menuAtions[0].Contains("Article as PDF"));
                Assert.IsTrue(menuAtions[1].Contains("Article as EPUB"));
                Assert.IsTrue(menuAtions[2].Contains("Print this Article"));
                Assert.IsTrue(menuAtions[3].Contains("Email To Colleague"));
                Assert.IsTrue(menuAtions[4].Contains("Add to My Favorites"));
                Assert.IsTrue(menuAtions[5].Contains("Export to Citation Manager"));
                Assert.IsTrue(menuAtions[6].Contains("Alert Me When Cited"));
                Assert.IsTrue(menuAtions[7].Contains("Get Content"));
                Assert.IsTrue(menuAtions[8].Contains("View Images in Gallery"));
                Assert.IsTrue(menuAtions[9].Contains("View Images in Slideshow"));
                Assert.IsTrue(menuAtions[10].Contains("Export All Images to PowerPoint File"));
            }
        }

        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstCurrentIssueLinksExist()
        {
            //TEST SETUP//
            var driver = TestData.DataForTstCurrentIssueLinksExist.Driver;
            driver.Url = TestData.DataForTstCurrentIssueLinksExist.StartUrl;

            //TEST
            using (driver)
            {
                JournalPage journalPage = new JournalPage(driver);
                journalPage.NavigateToCurrentIssue();
                CurrentIssuePage currentIssuePage = new CurrentIssuePage(driver);
                var listOfInnerHTML = currentIssuePage.GetIssueLinks();
                Assert.IsTrue(listOfInnerHTML[0].Contains("Table of Contents Outline"));
                Assert.IsTrue(listOfInnerHTML[1].Contains("Subscribe to eTOC"));
                Assert.IsTrue(listOfInnerHTML[2].Contains("View Contributor Index"));
            }
            
        }

        //Articles by title with cme withing all dates
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstAdvSearchCase1()
        {
            //TEST SETUP//
            var driver = TestData.DataForTstAdvSearchCase1.Driver;
            var qsOptions = TestData.DataForTstAdvSearchCase1.QsOptions;
            var fqOptions = TestData.DataForTstAdvSearchCase1.FqOptions;
            var products = TestData.DataForTstAdvSearchCase1.Products;
            var sRequest = TestData.DataForTstAdvSearchCase1.SRequest;
            driver.Url = TestData.DataForTstAdvSearchCase1.StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(sRequest);

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
                searchPage.SearchButton.Click();
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(@"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div")));
                SearchResultPage resultsPage = new SearchResultPage(driver);
                resultsPage.SelectSortByOption(fqOptions.sorting);
                System.Threading.Thread.Sleep(2000);
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
            var driver = TestData.DataForTstAdvSearchCase2.Driver;
            var qsOptions = TestData.DataForTstAdvSearchCase2.QsOptions;
            var fqOptions = TestData.DataForTstAdvSearchCase2.FqOptions;
            var products = TestData.DataForTstAdvSearchCase2.Products;
            var sRequest = TestData.DataForTstAdvSearchCase2.SRequest;
            driver.Url = TestData.DataForTstAdvSearchCase2.StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(sRequest);
            
            //TEST
            //Results count from API
            int countS = searchResponse.TotalFound;
            //Results count from UI
            int countW;
            using (driver)
            {
                AdvSearchPage searchPage = new AdvSearchPage(driver);
                searchPage.SelectSearchOptions(qsOptions, fqOptions, products);
                searchPage.SearchButton.Click();
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
            var driver = TestData.DataForTstAdvSearchCase3.Driver;
            var qsOptions = TestData.DataForTstAdvSearchCase3.QsOptions;
            var fqOptions = TestData.DataForTstAdvSearchCase3.FqOptions;
            var products = TestData.DataForTstAdvSearchCase3.Products;
            var sRequest = TestData.DataForTstAdvSearchCase3.SRequest;
            driver.Url = TestData.DataForTstAdvSearchCase3.StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(sRequest);

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
                searchPage.SearchButton.Click();
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(@"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div")));
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

        //Articles by all keys with open access withing all dates
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstAdvSearchCase4()
        {
            //TEST SETUP//
            var driver = TestData.DataForTstAdvSearchCase4.Driver;
            var qsOptions = TestData.DataForTstAdvSearchCase4.QsOptions;
            var fqOptions = TestData.DataForTstAdvSearchCase4.FqOptions;
            var products = TestData.DataForTstAdvSearchCase4.Products;
            var sRequest = TestData.DataForTstAdvSearchCase4.SRequest;
            driver.Url = TestData.DataForTstAdvSearchCase4.StartUrl;
            var searchResponse = SolrWorker.GetSearchResults(sRequest);

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
                searchPage.SearchButton.Click();
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(@"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div")));
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Test]
        public void TstRequest()
        {
            //TEST SETUP//
            QueryStringOptions qsOptions = new QueryStringOptions();
            FilterQueriesOptions fqOptions = new FilterQueriesOptions(_articles: true, _cme:true, _sorting:SortByOptionsEnum.Newest);
            string[] products = { "ccme" };
            var sRequest = SolrRequest.GenerateRequest(qsOptions, fqOptions, products);

            //TEST
            var searchResponse = SolrWorker.GetSearchResults(sRequest);
        }

        [Test]
        public void TstUi()
        {   
            //TEST SETUP//
            string methodName = "TstUi";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, Browsers.FireFox.ToString());

            _driver.Url = "http://journals.lww.com/ccmjournal/pages/advancedsearch.aspx";
            QueryStringOptions qsOptions = new QueryStringOptions(_title: "A");
            FilterQueriesOptions fqOptions = new FilterQueriesOptions(_articles: true, _cme: true, _sorting: SortByOptionsEnum.Newest);
            string[] products = { "ccme" };

            //TEST
            using (_driver)
            {
                AdvSearchPage searchPage = new AdvSearchPage(_driver);
                searchPage.SelectSearchOptions(qsOptions, fqOptions, products);
                searchPage.SearchButton.Click();
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(@"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div")));
                SearchResultPage resultsPage = new SearchResultPage(_driver);
                resultsPage.SelectSortByOption(fqOptions.sorting);
                System.Threading.Thread.Sleep(2000);
                resultsPage.GetAriclesIdsUiWithoutPap();
            }
        }
    }
}
