using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Pages;

namespace Tests.Task_1
{
    public class Tests_Task1 : SetUp_Task1
    {
        public Tests_Task1(string profile, string environment) : base(profile, environment) { }

        private static string StartUrl;
        private static string Login = "igor_neslukhovski@epam.com";
        private static string Pass = "epam_test1";

        [Test]
        public void TstLogIn()
        {
            StartUrl = "http://journals.lww.com/";
            Driver.Url = StartUrl;
            LoginPage loginPage = new LoginPage(Driver);
            loginPage.Login(Login, Pass);
            Wait.Until(ExpectedConditions.ElementIsVisible(UserActionsToolBarPage.LogOutButtonBy));
        }

        [Test]
        public void TstMenuElementsExist()
        {
            StartUrl = "http://journals.lww.com/asaiojournal";
            Driver.Url = StartUrl;
            JournalPage journalPage = new JournalPage(Driver);
            journalPage.ArticlesContainer.FindFreeOrOpenArticle().Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(ArticlePage.AddToMyCollectionsIconBy));
            ArticlePage articlePage = new ArticlePage(Driver);
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
        public void TstCurrentIssueLinksExist()
        {
            StartUrl = "http://journals.lww.com/ccmjournal/";
            //StartUrl = "http://journals.lww.com/annalsplasticsurgery/"; //Only FREE article
            Driver.Url = StartUrl;
            JournalPage journalPage = new JournalPage(Driver);
            journalPage.NavigateToCurrentIssue();
            Wait.Until(ExpectedConditions.ElementIsVisible(CurrentIssuePage.SubscribeTocBy));
            CurrentIssuePage currentIssuePage = new CurrentIssuePage(Driver);
            var listOfInnerHTML = currentIssuePage.GetIssueLinks();
            Assert.IsTrue(listOfInnerHTML[0].Contains("Table of Contents Outline"));
            Assert.IsTrue(listOfInnerHTML[1].Contains("Subscribe to eTOC"));
            Assert.IsTrue(listOfInnerHTML[2].Contains("View Contributor Index"));
        }
    }
}
