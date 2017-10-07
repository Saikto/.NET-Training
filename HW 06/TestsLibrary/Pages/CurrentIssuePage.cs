using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary
{
    public class CurrentIssuePage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = @"//*[@class=""issueActions""]")]
        private IWebElement IssueActions;

        public CurrentIssuePage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public List<string> GetIssueLinks()
        {
            List<string> listOfInnerHTML = new List<string>();
            listOfInnerHTML.Add(IssueActions.FindElement(By.ClassName("actionTOC")).FindElement(By.TagName("a"))
                .GetAttribute("innerHTML"));
            listOfInnerHTML.Add(IssueActions.FindElement(By.ClassName("subscribeTOC")).FindElement(By.TagName("a"))
                .GetAttribute("innerHTML"));
            listOfInnerHTML.Add(IssueActions.FindElement(By.ClassName("contributorIndex")).FindElement(By.TagName("a"))
                .GetAttribute("innerHTML"));
            return listOfInnerHTML;
        }
    }
}
