﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Pages;

namespace BrowserStack.Task_1
{
    [TestFixture("single", "Win7FireFox")]
    [TestFixture("single", "Win10Chrome")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class TestsTask1 : BrowserStackNUnitTest
    {
        public TestsTask1(string profile, string environment) : base(profile, environment) { }

        //private static IWebDriver driver = TestDataTask1.Driver;
        private static WebDriverWait wait = TestDataTask1.Wait;
        
        [SetUp] public void 
        [Test]
        public void TstLogInBS()
        {
            //TEST SETUP//
            var login = TestDataTask1.DataForTstLogIn.Login;
            var pass = TestDataTask1.DataForTstLogIn.Pass;
            driver.Url = TestDataTask1.DataForTstLogIn.StartUrl;

            //TEST
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login(login, pass);
            wait.Until(ExpectedConditions.ElementIsVisible(UserActionsToolBarPage.LogOutButtonBy));
        }

        [Test]
        public void TstMenuElementsExistBS()
        {
            //TEST SETUP//
            driver.Url = TestDataTask1.DataForTstMenuElementsExist.StartUrl;

            //TEST
            JournalPage journalPage = new JournalPage(driver);
            journalPage.ArticlesContainer.FindFreeOrOpenArticle().Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(ArticlePage.AddToMyCollectionsIconBy));
            ArticlePage articlePage = new ArticlePage(driver);
            List<string> menuActions = articlePage.GetArticleMenu().Select(t => t.FindElement(By.TagName("a")).GetAttribute("innerHTML")).ToList();

            Assert.IsTrue(menuActions[0].Contains("Article as PDF"));
            Assert.IsTrue(menuActions[1].Contains("Article as EPUB"));
            Assert.IsTrue(menuActions[2].Contains("Print this Article"));
            Assert.IsTrue(menuActions[3].Contains("Email To Colleague"));
            Assert.IsTrue(menuActions[4].Contains("Add to My Favorites"));
            Assert.IsTrue(menuActions[5].Contains("Export to Citation Manager"));
            Assert.IsTrue(menuActions[6].Contains("Alert Me When Cited"));
            Assert.IsTrue(menuActions[7].Contains("Get Content"));
            Assert.IsTrue(menuActions[8].Contains("View Images in Gallery"));
            Assert.IsTrue(menuActions[9].Contains("View Images in Slideshow"));
            Assert.IsTrue(menuActions[10].Contains("Export All Images to PowerPoint File"));
        }

        [Test]
        public void TstCurrentIssueLinksExistBS()
        {
            //TEST SETUP//
            driver.Url = TestDataTask1.DataForTstCurrentIssueLinksExist.StartUrl;

            //TEST
            JournalPage journalPage = new JournalPage(driver);
            journalPage.NavigateToCurrentIssue();
            wait.Until(ExpectedConditions.ElementIsVisible(CurrentIssuePage.SubscribeTocBy));
            CurrentIssuePage currentIssuePage = new CurrentIssuePage(driver);
            var listOfInnerHTML = currentIssuePage.GetIssueLinks();
            Assert.IsTrue(listOfInnerHTML[0].Contains("Table of Contents Outline"));
            Assert.IsTrue(listOfInnerHTML[1].Contains("Subscribe to eTOC"));
            Assert.IsTrue(listOfInnerHTML[2].Contains("View Contributor Index"));
        }
    }
}
