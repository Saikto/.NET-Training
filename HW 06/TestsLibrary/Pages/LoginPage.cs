using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class LoginPage
    {
        private IWebDriver _driver;
   
        public static By UserNameFieldBy = By.XPath(@"//input[contains(@id, ""UserName"")]");
        public static By PasswordFieldBy = By.XPath(@"//input[contains(@id, ""Password"")]");
        public static By SubmitButtonBy = By.XPath(@"//input[contains(@id, ""LoginButton"")]");

        private IWebElement UserNameField;
        private IWebElement PasswordField;
        private IWebElement SubmitButton;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            UserNameField = _driver.FindElement(UserNameFieldBy);
            PasswordField = _driver.FindElement(PasswordFieldBy);
            SubmitButton = _driver.FindElement(SubmitButtonBy);
        }

        public void Login(string username, string password)
        {
            UserNameField.SendKeys(username);
            PasswordField.SendKeys(password);
            SubmitButton.Click();
        }
    }
}
