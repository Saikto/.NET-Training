using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class CurrentIssuePage
    {
        private IWebDriver _driver;

        public By issueActionsBy = By.XPath(@"//*[@class=""issueActions""]");
        public By actionTOCBy = By.XPath(@"//*[@class=""actionTOC""]");
        public By subscribeTOCBy = By.XPath(@"//*[@class=""subscribeTOC""]");
        public By contributorIndexBy = By.XPath(@"//*[@class=""contributorIndex""]");

        private IWebElement issueActions;
        private IWebElement actionTOC;
        private IWebElement subscribeTOC;
        private IWebElement contributorIndex;

        public CurrentIssuePage(IWebDriver driver)
        {
            _driver = driver;
            issueActions = _driver.FindElement(issueActionsBy);
            actionTOC = issueActions.FindElement(actionTOCBy);
            subscribeTOC = issueActions.FindElement(subscribeTOCBy);
            contributorIndex = issueActions.FindElement(contributorIndexBy);
        }

        public List<string> GetIssueLinks()
        {
            List<string> listOfInnerHTML = new List<string>();
            listOfInnerHTML.Add(actionTOC.FindElement(By.TagName("a"))
                                .GetAttribute("innerHTML"));
            listOfInnerHTML.Add(subscribeTOC.FindElement(By.TagName("a"))
                                .GetAttribute("innerHTML"));
            listOfInnerHTML.Add(contributorIndex.FindElement(By.TagName("a"))
                                .GetAttribute("innerHTML"));
            return listOfInnerHTML;
        }
    }
}
