using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using TestsLibrary.Enums;
using TestsLibrary.Utils;

namespace TestsLibrary.Pages
{
    public class SearchResultPage
    {
        private readonly IWebDriver _driver;

        public static By ResultCountBy = By.XPath(@"//*[@class=""resultCount""]");
        public static By SortByDropDownListBy = By.XPath(@"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div");
        public static By SortOptionBestMatchBy = By.XPath(@"//*[contains(text(), ""Best Match"")]");
        public static By SortOptionNewestBy = By.XPath(@"//*[contains(text(), ""Newest"")]");
        public static By SortOptionOldestBy = By.XPath(@"//*[contains(text(), ""Oldest"")]");

        private AcrticlesContainer ArticlesContainer => new AcrticlesContainer(_driver);
        private IWebElement ResultCount => _driver.FindElement(ResultCountBy);
        private IWebElement SortByDropDownList => _driver.FindElement(SortByDropDownListBy);
        private IWebElement SortOptionBestMatch => _driver.FindElement(SortOptionBestMatchBy);
        private IWebElement SortOptionNewest => _driver.FindElement(SortOptionNewestBy);
        private IWebElement SortOptionOldest => _driver.FindElement(SortOptionOldestBy);


        public SearchResultPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GetTitlesAndIds(out List<string> titles, out List<string> ids)
        {
            var listOfArticles = ArticlesContainer.GetArticlesList();
            List<string> uiTitles = new List<string>();
            List<string> uiIds = new List<string>();
            foreach (var article in listOfArticles)
            {
                if (!article.Href.Contains("imagegallery") && !article.IsPap())
                {
                    uiIds.Add(HrefParser.ParseArticleHrefToId(article.Href));
                    uiTitles.Add(article.Title);
                }
                if (article.Href.Contains("imagegallery"))
                {
                    uiTitles.Add("");
                    uiIds.Add(HrefParser.ParseImageHrefToId(article.Href));
                    //uiTitles.Add(article.Title);
                }
            }
            titles = uiTitles;
            ids = uiIds;
        }

        public int GetResultCount()
        {
            if (Int32.TryParse(ResultCount.Text.Split()[0], out int count))
                return count;
            throw new NotFoundException();
        }

        public void SelectSortByOption(SortByOptionsEnum option)
        {
            SortByDropDownList.Click();
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
        }
    }
}
