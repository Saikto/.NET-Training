using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using TestsLibrary;
using NUnit.Framework;
using OpenQA.Selenium;

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
            string methodName = "TstLogIn";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, _browser);

            _driver.Url = "http://journals.lww.com";
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
            string methodName = "TstMenuElementsExist";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, _browser);

            //_driver.Url = "http://journals.lww.com/annalsplasticsurgery"; //Only FREE article
            _driver.Url = "http://journals.lww.com/asaiojournal"; //Only Open article
            using (_driver)
            {
                JournalPage journalPage = new JournalPage(_driver);
                try
                {
                    journalPage.FindOpenArticle().Click();
                }
                catch (NoSuchElementException)
                {
                    IWebElement freeArticle = journalPage.FindFreeArticle();
                    if (freeArticle == null)
                    {
                        Assert.Fail("Open or free article where not found.");
                    }
                    else
                    {
                        freeArticle.Click();
                    }
                }
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
            string methodName = "TstCurrentIssueLinksExist";
            IWebDriver _driver = WebDriverSelector.GetWebDriver(methodName, _browser);
            _driver.Url = "http://journals.lww.com/asaiojournal";
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
    }
}
