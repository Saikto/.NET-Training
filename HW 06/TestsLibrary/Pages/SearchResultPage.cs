using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using TestsLibrary.Enums;
using TestsLibrary.Utils;

namespace TestsLibrary.Pages
{
    public class SearchResultPage
    {
        private IWebDriver _driver;

        public static By ResultCountBy = By.XPath(@"//*[@class=""resultCount""]");
        public static By SortByDropDownListBy = By.XPath(@"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div");
        public static By SortOptionBestMatchBy = By.XPath(@"//*[contains(text(), ""Best Match"")]");
        public static By SortOptionNewestBy = By.XPath(@"//*[contains(text(), ""Newest"")]");
        public static By SortOptionOldestBy = By.XPath(@"//*[contains(text(), ""Oldest"")]");

        private AcrticlesContainer ArticlesContainer;
        private IWebElement ResultCount;
        private IWebElement SortByDropDownList;
        private IWebElement SortOptionBestMatch;
        private IWebElement SortOptionNewest;
        private IWebElement SortOptionOldest;


        public SearchResultPage(IWebDriver driver)
        {
            _driver = driver;
            ResultCount = _driver.FindElement(ResultCountBy);
            ArticlesContainer = new AcrticlesContainer(_driver);
            SortByDropDownList = _driver.FindElement(SortByDropDownListBy);
        }

        public void GetTitlesAndIds(out List<string> titles, out List<string> ids)
        {
            var listOfArticles = ArticlesContainer.GetArticlesList();
            List<string> uiTitles = new List<string>();
            List<string> uiIds = new List<string>();
            foreach (var article in listOfArticles)
            {
                if (article.Href.Contains("imagegallery"))
                {
                    uiIds.Add(HrefParser.ParseImageHrefToId(article.Href));
                    uiTitles.Add(article.Title);
                }
                if (!article.Href.Contains("imagegallery") && !article.IsPap())
                {
                    uiTitles.Add("");
                    uiIds.Add(HrefParser.ParseArticleHrefToId(article.Href));
                    //uiTitles.Add(article.Title);
                }
            }
            titles = uiTitles;
            ids = uiIds;
        }

        public int GetResultCount()
        {
            ResultCount = _driver.FindElement(ResultCountBy);
            if (Int32.TryParse(ResultCount.Text.Split()[0], out int count))
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
            ArticlesContainer = new AcrticlesContainer(_driver);
        }
    }
}
