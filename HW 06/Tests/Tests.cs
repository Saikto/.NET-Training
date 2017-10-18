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
    public enum Browsers {
        Chrome,
        FireFox,
        InternetExplorer,
        Edge
    }
    //asaiojournal
    public class Tests
    {
        private string _browser = Browsers.FireFox.ToString();
        private string _login = "igor_neslukhovski@epam.com";
        private string _pass = "epam_test1";

        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstLogIn()
        {
            //WebDriver setup
            string methodName = "TstLogIn";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, _browser);
            _driver.Url = "http://journals.lww.com";
            
            //Test
            using (_driver)
            {
                LoginPage loginPage = new LoginPage(_driver);
                loginPage.UserNameField.SendKeys(_login);
                loginPage.PasswordField.SendKeys(_pass);
                loginPage.SubmitButton.Click();
                _driver.FindElement(By.Id("ctl00_ucUserActionsToolbar_lnkLogout"));
            }
        }

        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TstMenuElementsExist()
        {
            //WebDriver setup
            string methodName = "TstMenuElementsExist";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, _browser);
            //_driver.Url = "http://journals.lww.com/annalsplasticsurgery"; //Only FREE article
            _driver.Url = "http://journals.lww.com/asaiojournal"; //Only Open article
            
            //Test
            using (_driver)
            {
                JournalPage journalPage = new JournalPage(_driver);
                journalPage.FindOpenArticle().Click();
                //journalPage.FindFreeArticle().Click();
                ArticlePage articlePage = new ArticlePage(_driver);
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
            //WebDriver setup
            string methodName = "TstCurrentIssueLinksExist";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, _browser);
            _driver.Url = "http://journals.lww.com/asaiojournal";
            
            //Test
            using (_driver)
            {
                JournalPage journalPage = new JournalPage(_driver);
                journalPage.NavigateToCurrentIssue();
                CurrentIssuePage currentIssuePage = new CurrentIssuePage(_driver);
                var listOfInnerHTML = currentIssuePage.GetIssueLinks();
                Assert.IsTrue(listOfInnerHTML[0].Contains("Table of Contents Outline"));
                Assert.IsTrue(listOfInnerHTML[1].Contains("Subscribe to eTOC"));
                Assert.IsTrue(listOfInnerHTML[2].Contains("View Contributor Index"));
            }
            
        }

        //Articles with cme withing all dates
        [Test]
        public void TstAdvSearchCase1()
        {
            //WebDriver setup
            string methodName = "TstAdvSearchCase1";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, _browser);
            //_driver.Url = "http://journals.lww.com/aacr/pages/advancedsearch.aspx";
            _driver.Url = "http://journals.lww.com/plasreconsurg/pages/advancedsearch.aspx";
            //Search options setup
            QueryStringOptions qsOptions = new QueryStringOptions(_title:"A");
            FilterQueriesOptions fqOptions = new FilterQueriesOptions(_articles:true, _cme:true,
                _pDate:PublicationDateEnum.AllDates, _sorting:SortByOptionsEnum.Newest);
            string[] products = { "PRECOS" };
            
            //Test
            //Titles from ui, exepting PAP
            List<string> titlesUi;
            using (_driver)
            {
                AdvSearchPage searchPage = new AdvSearchPage(_driver);
                searchPage.SelectSearchOptions(qsOptions, fqOptions, products);
                searchPage.SearchButton.Click();
                SearchResultPage resultsPage = new SearchResultPage(_driver);
                resultsPage.SelectSortByOption(fqOptions.sorting);
                titlesUi = resultsPage.GetResultTitlesWithoutPAPList(fqOptions.rowsToGet);
            }
            //Titles from api, exepting PAP
            var sRequest = SolrRequest.GenerateRequest(qsOptions, fqOptions, products);
            var searchResponse = SolrWorker.GetSearchResults(sRequest);
            var resultsApi = searchResponse.Results.Where(r => r.GetPublishDate()[0] != 9000).ToList();
            bool a = TitlesComparer.AreTitlesEqual(titlesUi, resultsApi);
            Assert.AreEqual(true, a);
        }

        [Test]
        public void TstAdvSearch()
        {
            string methodName = "TstAdvSearch";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, _browser);
            _driver.Url = "http://journals.lww.com/aacr/pages/advancedsearch.aspx";
            QueryStringOptions qsOptions = new QueryStringOptions();
            FilterQueriesOptions fqOptions = new FilterQueriesOptions();
            string[] products = { "aacr" };

            using (_driver)
            {
                AdvSearchPage searchPage = new AdvSearchPage(_driver);
                //searchPage.SelectSearchOptions("blood", "adv", false, true, true, false, true, false, true);
                searchPage.SelectSearchOptions(qsOptions, fqOptions);
                searchPage.SearchButton.Click();
                SearchResultPage resultsPage = new SearchResultPage(_driver);
                int count = resultsPage.GetResultCount();
                resultsPage.SelectSortByOption(SortByOptionsEnum.Newest);
            }
        }

        //All key words aricles and images withing last five years
        [Test]
        public void TstAdvSearhCase2()
        {
            string methodName = "TstAdvSearhCase2";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, _browser);
            _driver.Url = "http://journals.lww.com/aacr/pages/advancedsearch.aspx";
            QueryStringOptions qsOptions = new QueryStringOptions(_qAllKeys: "a");
            FilterQueriesOptions fqOptions = new FilterQueriesOptions(_articles:true, _image:true, _pDate:PublicationDateEnum.Last5Years, _sorting:SortByOptionsEnum.BestMatch);
            string[] products = { "aacr" };

            var sRequest = SolrRequest.GenerateRequest(qsOptions,fqOptions, products);
            var searchResponse = SolrWorker.GetSearchResults(sRequest);
            int countS = searchResponse.TotalFound;
            using (_driver)
            {
                AdvSearchPage searchPage = new AdvSearchPage(_driver);
                searchPage.SelectSearchOptions(qsOptions, fqOptions, products);
                searchPage.SearchButton.Click();
                SearchResultPage resultsPage = new SearchResultPage(_driver);
                int countW = resultsPage.GetResultCount();
                Assert.AreNotEqual(0, countS);
                Assert.AreNotEqual(0, countW);
                Assert.AreEqual(countS, countW);
            }
            
        }

        [Test]
        public void TstSolrEquality()
        {
            FilterQueriesOptions fqOptions = new FilterQueriesOptions();
            QueryStringOptions qsOptions = new QueryStringOptions();
            string[] products = { "aacr" };

            var sRequest = SolrRequest.GenerateRequest(qsOptions, fqOptions, products);
            var searchResponse = SolrWorker.GetSearchResults(sRequest);
            var withoutPAP = searchResponse.Results.Select(r => r.GetPublishDate()[0] != 9000).ToList();
        }

    }
}
