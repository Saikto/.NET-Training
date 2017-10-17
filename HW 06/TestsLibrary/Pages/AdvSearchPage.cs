using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestsLibrary.Enums;
using TestsLibrary.Models;

namespace TestsLibrary.Pages
{
    public class AdvSearchPage
    {
        private IWebDriver _driver;

        public IWebElement SearchButton
        {
            get
            {
                var spanElement = _driver.FindElement(By.ClassName("buttonsDiv"));
                var element = spanElement.FindElements(By.TagName("input"))
                    .First(t => t.GetAttribute("type") == "submit");
                return element;
            }
        }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""keywords_input_1""]")]
        public IWebElement AllKeyWordsField;

        [FindsBy(How = How.XPath, Using = @"//*[@id=""keywords_input_2""]")]
        public IWebElement TitleField;

        public IWebElement ContentTypeArticleCheckBox
        {
            get
            {
                var spanElement = _driver.FindElements(By.ClassName("asb-content-inp"));
                var element = spanElement.First(e =>
                    e.FindElement(By.TagName("input")).GetAttribute("id").EndsWith("filterListArticle"));
                return element;
            }
        }

        public IWebElement ContentTypeImageCheckBox
        {
            get
            {
                var spanElement = _driver.FindElements(By.ClassName("asb-content-inp"));
                var element = spanElement.First(e =>
                    e.FindElement(By.TagName("input")).GetAttribute("id").EndsWith("filterListImage"));
                return element;
            }
        }

        public IWebElement CMECheckBox
        {
            get
            {
                var spanElement = _driver.FindElements(By.ClassName("asb-limit-content-inp"));
                var element = spanElement.First(e =>
                    e.FindElement(By.TagName("input")).GetAttribute("id").EndsWith("filterListCME"));
                return element;
            }
        }

        public IWebElement AllDatesRadio
        {
            get
            {
                var spanElement = _driver.FindElement(By.ClassName("pubDates"));
                var element = spanElement.FindElements(By.TagName("input")).First(t => t.GetAttribute("value") == "AllIssues");
                return element;
            }
        }

        public IWebElement LastFiveYearsRadio
        {
            get
            {
                var spanElement = _driver.FindElement(By.ClassName("pubDates"));
                var element = spanElement.FindElements(By.TagName("input"))
                    .First(t => t.GetAttribute("value") == "Last5Years");
                return element;
            }
        }

        public IWebElement AllArticleTypesRadio
        {
            get
            {
                var spanElement = _driver.FindElement(By.ClassName("articleAccessOptions"));
                var element = spanElement.FindElements(By.TagName("input"))
                    .First(t => t.GetAttribute("value") == "All");
                return element;
            }
        }

        public IWebElement OpenAccessOnlyRadio
        {
            get
            {
                var spanElement = _driver.FindElement(By.ClassName("articleAccessOptions"));
                var element = spanElement.FindElements(By.TagName("input"))
                    .First(t => t.GetAttribute("value") == "OpenAccess");
                return element;
            }
        }

        public AdvSearchPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public void SelectSearchOptions(QueryStringOptions qso, FilterQueriesOptions fqo, params string[] prods)
        {
            AllKeyWordsField.SendKeys(qso.qAllKeys);
            TitleField.SendKeys(qso.title);

            if (!ContentTypeImageCheckBox.Selected && fqo.image)
            {
                ContentTypeImageCheckBox.Click();
            }
            else if (ContentTypeImageCheckBox.Selected && !fqo.image)
            {
                ContentTypeImageCheckBox.Click();
            }

            if (!fqo.articles && fqo.image)
            {
                ContentTypeArticleCheckBox.Click();
            }

            if (!CMECheckBox.Selected && fqo.cme)
            {
                CMECheckBox.Click();
            }
            else if (CMECheckBox.Selected && !fqo.cme)
            {
                CMECheckBox.Click();
            }

            if (fqo.pDate == PublicationDateEnum.AllDates)
                AllDatesRadio.Click();
            if(fqo.pDate == PublicationDateEnum.Last5Years)
                LastFiveYearsRadio.Click();

            if (fqo.openAccess && AllArticleTypesRadio.Enabled)
                OpenAccessOnlyRadio.Click();

            //Deselecting unnecessary journals
            if (prods.Length == 1)
            {
                var journalsToSearchIn = _driver.FindElements(By.ClassName("asb-journals-row"));
                if (journalsToSearchIn.Count > 1)
                {
                    for (int i = 1; i < journalsToSearchIn.Count; i++)
                    {
                        IWebElement e = journalsToSearchIn[i].FindElement(By.TagName("input"));
                        if(e.Selected)
                            e.Click();
                    }
                }
            }
        }
    }
}
