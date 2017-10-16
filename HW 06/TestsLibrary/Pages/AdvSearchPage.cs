using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

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

        public void SelectSearchOptions(string allKeyWords = "", string title = "", bool searchArticles = true, bool searchImages = false, bool cme = false,
            bool allDates = true, bool fiveYears = false, bool allArticleTypes = true, bool openAccess = false)
        {
            if (allDates == fiveYears)
                throw new ArgumentException();
            if(allArticleTypes == openAccess)
                throw new ArgumentException();
            AllKeyWordsField.SendKeys(allKeyWords);
            TitleField.SendKeys(title);

            if (ContentTypeArticleCheckBox.GetAttribute("checked") == "checked" && !searchArticles)
            {
                ContentTypeArticleCheckBox.Click();
            }

            if (!ContentTypeImageCheckBox.Selected && searchImages)
            {
                ContentTypeImageCheckBox.Click();
            }
            else if (ContentTypeImageCheckBox.Selected && !searchImages)
            {
                ContentTypeImageCheckBox.Click();
            }

            if (!CMECheckBox.Selected && cme)
            {
                CMECheckBox.Click();
            }
            else if (CMECheckBox.Selected && !cme)
            {
                CMECheckBox.Click();
            }

            if (allDates)
                AllDatesRadio.Click();
            if(fiveYears)
                LastFiveYearsRadio.Click();

            if (allArticleTypes)
                AllArticleTypesRadio.Click();
            if (openAccess)
                OpenAccessOnlyRadio.Click();
        }
    }
}
