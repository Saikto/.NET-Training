using OpenQA.Selenium;
using TestsLibrary.Enums;
using TestsLibrary.Models;

namespace TestsLibrary.Pages
{
    public class AdvSearchPage
    {
        private readonly IWebDriver _driver;

        public By SearchButtonBy = By.XPath(@"//input[contains(@name, ""searchAgain"")]");
        public By AllKeyWordsFieldBy = By.XPath(@"//input[@id=""keywords_input_1""]");
        public By TitleFieldBy = By.XPath(@"//input[@id=""keywords_input_2""]");
        public By ContentTypeArticleCheckBoxBy = By.XPath(@"//input[contains(@name, ""filterListArticle"")]");
        public By ContentTypeImageCheckBoxBy = By.XPath(@"//input[contains(@name, ""filterListImage"")]");
        public By CmeCheckBoxBy = By.XPath(@"//input[contains(@name, ""filterListCME"")]");
        public By AllDatesRadioBy = By.XPath(@"//input[contains(@id, ""searchDatesRadioButtonList_0"")]");
        public By LastFiveYearsRadioBy = By.XPath(@"//input[contains(@id, ""searchDatesRadioButtonList_4"")]");
        public By AllArticleTypesRadioBy = By.XPath(@"//input[contains(@id, ""articleAccessRadioButtonList_0"")]");
        public By OpenAccessOnlyRadioBy = By.XPath(@"//input[contains(@id, ""articleAccessRadioButtonList_1"")]");

        private IWebElement SearchButton => _driver.FindElement(SearchButtonBy);
        private IWebElement AllKeyWordsField => _driver.FindElement(AllKeyWordsFieldBy);
        private IWebElement TitleField => _driver.FindElement(TitleFieldBy);
        private IWebElement ContentTypeArticleCheckBox => _driver.FindElement(ContentTypeArticleCheckBoxBy);
        private IWebElement ContentTypeImageCheckBox => _driver.FindElement(ContentTypeImageCheckBoxBy);
        private IWebElement CmeCheckBox => _driver.FindElement(CmeCheckBoxBy);
        private IWebElement AllDatesRadio => _driver.FindElement(AllDatesRadioBy);
        private IWebElement LastFiveYearsRadio => _driver.FindElement(LastFiveYearsRadioBy);
        private IWebElement AllArticleTypesRadio => _driver.FindElement(AllArticleTypesRadioBy);
        private IWebElement OpenAccessOnlyRadio => _driver.FindElement(OpenAccessOnlyRadioBy);

        public AdvSearchPage(IWebDriver driver)
        {
            _driver = driver;
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

            if (!CmeCheckBox.Selected && fqo.cme)
            {
                CmeCheckBox.Click();
            }
            else if (CmeCheckBox.Selected && !fqo.cme)
            {
                CmeCheckBox.Click();
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

        public void GoSearching()
        {
            SearchButton.Click();
        }
    }
}
