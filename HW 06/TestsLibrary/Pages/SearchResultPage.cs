using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestsLibrary.Enums;

namespace TestsLibrary.Pages
{
    public class SearchResultPage
    {
        private IWebDriver _driver;
        
        [FindsBy(How = How.ClassName, Using = @"resultCount")]
        private IWebElement ResultCount;

        [FindsBy(How = How.ClassName, Using = @"wp-feature-articles")]
        private IWebElement Result;

        [FindsBy(How = How.XPath, Using = @"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div")]
        private IWebElement SortByDropDownList;

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

        public List<string> GetResultTitlesList()
        {
            List<string> titles = new List<string>();
            GetArticlesList().ForEach(r =>
                titles.Add(r.FindElement(By.XPath("//div[1]/div[1]/header[1]/h4[1]/a[1]")).GetAttribute("title")));
            //var titles = _driver.FindElements(By.XPath("//div[contains(@class, 'wp-feature-articles')]/div/article[1]/div[1]/div[1]/header[1]/h4[1]/a[1]"))
            //    .Select(x => x.GetAttribute("title"))
            //    .Take(20)
            //    .ToList();
            return titles;
        }

        public List<IWebElement> GetArticlesList()
        {
            var list = Result.FindElements(By.ClassName("article")).ToList();
            return list;
        }

        public List<string> GetResultPreviewsList()
        {
            var previews = _driver.FindElements(By.ClassName("article-previews"))
                .Select(x => x.GetAttribute("innerHTML"))
                .Take(20)
                .ToList();
            return previews;
        }


    }
}
