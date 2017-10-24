using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace TestsLibrary.Pages
{
    public class CurrentIssuePage
    {
        private readonly IWebDriver _driver;

        public static By IssueActionsBy = By.XPath(@"//*[@class=""issueActions""]");
        public static By ActionTocBy = By.XPath(@"//*[@class=""actionTOC""]");
        public static By SubscribeTocBy = By.XPath(@"//*[@class=""subscribeTOC""]");
        public static By ContributorIndexBy = By.XPath(@"//*[@class=""contributorIndex""]");

        public AcrticlesContainer ArticlesContainer => new AcrticlesContainer(_driver);
        private IWebElement IssueActions => _driver.FindElement(IssueActionsBy);
        private IWebElement ActionToc => IssueActions.FindElement(ActionTocBy);
        private IWebElement SubscribeToc => IssueActions.FindElement(SubscribeTocBy);
        private IWebElement ContributorIndex => IssueActions.FindElement(ContributorIndexBy);


        public CurrentIssuePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public List<string> GetIssueLinks()
        {
            List<string> listOfInnerHTML = new List<string>();
            listOfInnerHTML.Add(ActionToc.FindElement(By.TagName("a"))
                                .GetAttribute("innerHTML"));
            listOfInnerHTML.Add(SubscribeToc.FindElement(By.TagName("a"))
                                .GetAttribute("innerHTML"));
            listOfInnerHTML.Add(ContributorIndex.FindElement(By.TagName("a"))
                                .GetAttribute("innerHTML"));
            return listOfInnerHTML;
        }
    }
}
