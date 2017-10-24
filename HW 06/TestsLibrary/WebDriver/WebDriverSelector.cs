using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace TestsLibrary
{
    public static class WebDriverSelector
    {
        private static volatile Dictionary<string, IWebDriver> driversForTests = new Dictionary<string, IWebDriver>();

        public static IWebDriver GetWebDriver(string startedName, string browser, string profile = "", string enviroment = "")
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
                case "BrowserStack":
                    driver = GetBrowserStackDriver(profile, enviroment);
                    break;
                default:
                    driver = new FirefoxDriver();
                    break;
            }
            driversForTests.Add(startedName, driver);
            return driver;
        }

        private static IWebDriver GetBrowserStackDriver(string profile, string environment)
        {
            NameValueCollection caps = ConfigurationManager.GetSection("capabilities/" + profile) as NameValueCollection;
            NameValueCollection settings = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;

            DesiredCapabilities capability = new DesiredCapabilities();

            foreach (string key in caps.AllKeys)
            {
                capability.SetCapability(key, caps[key]);
            }

            foreach (string key in settings.AllKeys)
            {
                capability.SetCapability(key, settings[key]);
            }

            String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            if (username == null)
            {
                username = ConfigurationManager.AppSettings.Get("user");
            }

            String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            if (accesskey == null)
            {
                accesskey = ConfigurationManager.AppSettings.Get("key");
            }

            capability.SetCapability("browserstack.user", username);
            capability.SetCapability("browserstack.key", accesskey);

            IWebDriver driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), capability);

            return driver;
        }
    }
}
