using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestsLibrary.Enums;
using TestsLibrary.Utils;

namespace TestsLibrary.Pages
{
    public class SearchResultPage
    {
        private IWebDriver _driver;

        public By ResultCountBy = By.XPath(@"//*[@class=""resultCount""]");
        public By ResultBy = By.XPath(@"//*[@class=""wp-feature-articles""]");
        public By SortByDropDownListBy = By.XPath(@"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div");
        public By SortOptionBestMatchBy = By.XPath(@"//*[contains(text(), ""Best Match"")]");
        public By SortOptionNewestBy = By.XPath(@"//*[contains(text(), ""Newest"")]");
        public By SortOptionOldestBy = By.XPath(@"//*[contains(text(), ""Oldest"")]");

        private IWebElement ResultCount;
        private IWebElement Result;
        private IWebElement SortByDropDownList;
        private IWebElement SortOptionBestMatch;
        private IWebElement SortOptionNewest;
        private IWebElement SortOptionOldest;


        public SearchResultPage(IWebDriver driver)
        {
            _driver = driver;
            ResultCount = _driver.FindElement(ResultCountBy);
            Result = _driver.FindElement(ResultBy);
            SortByDropDownList = _driver.FindElement(SortByDropDownListBy);
        }

        public void GetTitlesAndIds(out List<string> titles, out List<string> ids)
        {
            var listOfArticles = Result.FindElements(By.TagName("article"));
            List<string> uiTitles = new List<string>();
            List<string> uiIds = new List<string>();
            List<string> headers = _driver.FindElements(By.TagName("h4")).Select(h => h.GetAttribute("innerHTML")).ToList();
            headers.RemoveAt(0);
            int i = 0;
            int j = 0;
            foreach (var header in headers)
            {
                if (header.Contains("imagegallery"))
                {
                    uiIds.Add(HrefParser.ParseImageHrefToId(_driver
                        .FindElements(By.XPath("//*[@id=\"ej-featured-article-info\"]/header/h4/a"))[i]
                        .GetAttribute("href")));
                    uiTitles.Add("");
                    i++;
                }
                if (!header.Contains("imagegallery"))
                {
                    var link = _driver.FindElements(By.XPath("//div[1]/div[1]/header[1]/h4[1]/a[1]"))[j];
                    if (link.FindElements(By.XPath("../../../ul[contains(@class, 'article-actions')]/li[contains(@id, 'PAP')]")).Count == 0)
                    {
                        uiIds.Add(HrefParser.ParseArticleHrefToId(link.GetAttribute("href")));
                        uiTitles.Add(link.GetAttribute("title"));
                    }
                    j++;
                }
            }
            titles = uiTitles;
            ids = uiIds;
        }

        public int GetResultCount()
        {
            if(Int32.TryParse(ResultCount.Text.Split()[0], out int count))
                return count;
            throw new NotFoundException();
        }

        public void SelectSortByOption(SortByOptionsEnum option)
        {
            SortByDropDownList.Click();
            SortOptionBestMatch = _driver.FindElement(SortOptionBestMatchBy);
            SortOptionNewest = _driver.FindElement(SortOptionNewestBy);
            SortOptionOldest = _driver.FindElement(SortOptionOldestBy);
            if (option == SortByOptionsEnum.BestMatch)
            {
                SortOptionBestMatch.Click();
            }
            if (option == SortByOptionsEnum.Newest)
            {
                SortOptionNewest.Click();
            }
            if (option == SortByOptionsEnum.Oldest)
            {
                SortOptionOldest.Click();
            }
            System.Threading.Thread.Sleep(4000);
            Result = _driver.FindElement(ResultBy);
        }
    }
}
