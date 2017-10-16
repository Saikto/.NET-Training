using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
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
