using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class SearchResultPage
    {
        private IWebDriver _driver;

        public enum SortByOptionsEnum
        {
            BestMatch,
            Newest,
            Oldest
        }

        [FindsBy(How = How.ClassName, Using = @"resultCount")]
        public IWebElement ResultCount;

        [FindsBy(How = How.XPath, Using = @"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div")]
        public IWebElement SortByDropDownList;

        public SearchResultPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public int GetResultCount()
        {
            if(Int32.TryParse(ResultCount.Text.Split()[0], out var count))
                return count;
            throw new NotFoundException();
        }

        public void SelectSortByOption(SortByOptionsEnum option)
        {
            SortByDropDownList.Click();

            if (option == SortByOptionsEnum.BestMatch)
            {
                SortByDropDownList.FindElement(
                    By.XPath("//*[@id=\"wpSearchResults\"]/div/div[2]/div[3]/div/div[2]/div/div[2]")).Click();
            }
            if (option == SortByOptionsEnum.Newest)
            {
                SortByDropDownList.FindElement(
                    By.XPath("//*[@id=\"wpSearchResults\"]/div/div[2]/div[3]/div/div[2]/div/div[2]")).Click();
            }
            if (option == SortByOptionsEnum.Oldest)
            {
                SortByDropDownList.FindElement(
                    By.XPath("//*[@id=\"wpSearchResults\"]/div/div[2]/div[3]/div/div[2]/div/div[3]")).Click();
            }


        }
    }
}
