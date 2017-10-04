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
    public class LoginPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "ctl00_ctl45_g_e504d159_38de_4cbf_9f4d_b2c12b300979_ctl00_txt_UserName")]
        public IWebElement UserNameField;

        [FindsBy(How = How.Id, Using = "ctl00_ctl45_g_e504d159_38de_4cbf_9f4d_b2c12b300979_ctl00_txt_Password")]
        public IWebElement PasswordField;

        [FindsBy(How = How.Id, Using = "ctl00_ctl45_g_e504d159_38de_4cbf_9f4d_b2c12b300979_ctl00_LoginButton")]
        public IWebElement SubmitButton;

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }
}
