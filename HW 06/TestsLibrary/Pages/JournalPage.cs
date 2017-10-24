using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class JournalPage
    {
        private readonly IWebDriver _driver;

        public static By CurrentIssueBy = By.XPath(@"//*[@id=""wpCurrentIssue""]");

        public AcrticlesContainer ArticlesContainer => new AcrticlesContainer(_driver);
        private IWebElement CurrentIssue => _driver.FindElement(CurrentIssueBy);

        public JournalPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateToCurrentIssue()
        {
            CurrentIssue.FindElement(By.TagName("a")).Click();
        }
    }
}
