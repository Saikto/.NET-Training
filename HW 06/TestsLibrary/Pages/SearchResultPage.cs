using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TestsLibrary.Enums;
using TestsLibrary.SOLR;
using TestsLibrary.Utils;

namespace TestsLibrary.Pages
{
    public class SearchResultPage
    {
        private IWebDriver _driver;
        
        [FindsBy(How = How.ClassName, Using = "resultCount")]
        private IWebElement ResultCount;

        [FindsBy(How = How.ClassName, Using = "wp-feature-articles")]
        private IWebElement Result;

        [FindsBy(How = How.XPath, Using = @"//*[@id=""wpSearchResults""]/div/div[2]/div[3]/div/div[1]/div")]
        private IWebElement SortByDropDownList;

        public SearchResultPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //public void GetTitlesAndIds(out List<string> titles, out List<string> ids)
        //{
        //    var listOfArticles = Result.FindElements(By.TagName("article"));
        //    List<string> uiTitles = new List<string>();
        //    List<string> uiIds = new List<string>();
        //    int i = 0;
        //    foreach (var article in listOfArticles)
        //    {
        //        if (article.FindElements(By.TagName("header"))[i]
        //                                    .GetAttribute("innerHTML")
        //                                    .Contains("imagegallery"))
        //        {
        //            uiIds.Add(HrefParser.ParseImageHrefToId(article
        //                                                        .FindElements(By.XPath("//*[@id=\"ej-featured-article-info\"]/header/h4/a"))[i]
        //                                                         .GetAttribute("href")));
        //            uiTitles.Add(article.FindElements(By.XPath("//*[@id=\"ej-featured-article-info\"]/header/h4/a"))[i]
        //                .GetAttribute("title"));
        //        }
        //        if(!article.FindElement(By.TagName("header"))
        //                 .GetAttribute("innerHTML").Contains("imagegallery"))
        //        {
        //            var link = article.FindElement(By.XPath("//div[1]/div[1]/header[1]/h4[1]/a[1]"));
        //            if (link.FindElements(By.XPath("../../../ul[contains(@class, 'article-actions')]/li[contains(@id, 'PAP')]")).Count == 0)
        //            {
        //                uiIds.Add(HrefParser.ParseArticleHrefToId(link.GetAttribute("href")));
        //                uiTitles.Add(link.GetAttribute("title"));
        //            }
        //        }
        //        i++;
        //    }
        //    titles = uiTitles;
        //    ids = uiIds;
        //}

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

        public List<string> GetTitilesUiWithoutPap(int n)
        {
            List<string> titlesWithoutPAP = _driver.FindElements(By.XPath("//div[contains(@class, 'wp-feature-articles')]/div/article[1]/div[1]/div[1]/header[1]/h4[1]/a[1]"))
                .Where(x => x.FindElements(By.XPath("../../../ul[contains(@class, 'article-actions')]/li[contains(@id, 'PAP')]")).Count == 0)
                .Select(x => x.GetAttribute("title"))
                .Take(n)
                .ToList();
            return titlesWithoutPAP;
        }

        public int GetResultCount()
        {
            if(Int32.TryParse(ResultCount.Text.Split()[0], out var count))
                return count;
            throw new NotFoundException();
        }

        public List<string> GetAriclesIdsUiWithoutPap()
        {
            var listOfHref = _driver.FindElements(By.XPath("//div[contains(@class, 'wp-feature-articles')]/div/article[1]/div[1]/div[1]/header[1]/h4[1]/a[1]"))
                .Where(x => x.FindElements(By.XPath("../../../ul[contains(@class, 'article-actions')]/li[contains(@id, 'PAP')]")).Count == 0)
                .Select(a => a.GetAttribute("href"))
                .ToList();
            var listOfIds = listOfHref.Select(HrefParser.ParseArticleHrefToId).ToList();
            return listOfIds;
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
