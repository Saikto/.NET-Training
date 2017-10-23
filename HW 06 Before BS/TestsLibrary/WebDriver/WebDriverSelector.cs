using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace TestsLibrary
{
    public static class WebDriverSelector
    {
        private static volatile Dictionary<string, IWebDriver> driversForTests = new Dictionary<string, IWebDriver>();
        public static IWebDriver GetWebDriver(string startedName, string browser)
        {
            IWebDriver driver;
            if (driversForTests.TryGetValue(startedName, out driver))
                return driver;
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "FireFox":
                    driver = new FirefoxDriver();
                    break;
                case "InternetExplorer":
                    driver = new InternetExplorerDriver();
                    break;
                case "Edge":
                    driver = new EdgeDriver();
                    break;
                default:
                    driver = new FirefoxDriver();
                    break;
            }
            driversForTests.Add(startedName, driver);
            return driver;
        }
    }
}
