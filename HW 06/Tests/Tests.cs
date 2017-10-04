using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using TestsLibrary;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Tests
{
    public enum Browsers {
        Chrome,
        FireFox,
        InternetExplorer,
        Edge
    }
    //asaiojournal
    public class Tests
    {
        private string _browser = Browsers.FireFox.ToString();
        private string _login = "igor_neslukhovski@epam.com";
        private string _pass = "epam_test1";
        private IWebDriver _driver;

        [SetUp]
        public void Initialize()
        {
            string fixtureName = "first";
            _driver = WebDriverSelector.GetWebDriver(fixtureName, _browser);
        }
        [Test]
        public void TstLogIn()
        {
            _driver.Url = "http://journals.lww.com";
            using (_driver)
            {
                LoginPage loginPage = new LoginPage(_driver);
                loginPage.UserNameField.SendKeys(_login);
                loginPage.PasswordField.SendKeys(_pass);
                loginPage.SubmitButton.Click();
                _driver.FindElement(By.Id("ctl00_ucUserActionsToolbar_lnkLogout"));
            }
        }

        [Test]
        public void MenuElementsExist()
        {

        }
    }
}
