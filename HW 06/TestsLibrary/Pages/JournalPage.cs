using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class JournalPage
    {
        private IWebDriver _driver;

        public static By CurrentIssueBy = By.XPath(@"//*[@id=""wpCurrentIssue""]");

        public AcrticlesContainer ArticlesContainer;
        private IWebElement CurrentIssue;

        public JournalPage(IWebDriver driver)
        {
            _driver = driver;
            ArticlesContainer = new AcrticlesContainer(_driver);
            CurrentIssue = _driver.FindElement(CurrentIssueBy);
        }

        public void NavigateToCurrentIssue()
        {
            CurrentIssue.FindElement(By.TagName("a")).Click();
        }
    }
}
