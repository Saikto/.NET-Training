using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace TestsLibrary
{
    public class WebDriverSelector
    {
        private static Dictionary<string, IWebDriver> driversForTests = new Dictionary<string, IWebDriver>();

        private WebDriverSelector()
        {
        }

        public static IWebDriver GetWebDriver(string methodName, string browser)
        {
            IWebDriver driver;
            if (driversForTests.TryGetValue(methodName, out driver))
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
            driversForTests.Add(methodName, driver);
            return driver;
        }
    }
}
