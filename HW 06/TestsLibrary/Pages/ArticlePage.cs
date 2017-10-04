using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary
{
    public class ArticlePage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "")]
        public IWebElement UserNameField;

        
        public ArticlePage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }
}
