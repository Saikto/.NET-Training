using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestsLibrary.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
   
        public static By UserNameFieldBy = By.XPath(@"//input[contains(@id, ""UserName"")]");
        public static By PasswordFieldBy = By.XPath(@"//input[contains(@id, ""Password"")]");
        public static By SubmitButtonBy = By.XPath(@"//input[contains(@id, ""LoginButton"")]");

        private IWebElement UserNameField => _driver.FindElement(UserNameFieldBy);
        private IWebElement PasswordField => _driver.FindElement(PasswordFieldBy);
        private IWebElement SubmitButton => _driver.FindElement(SubmitButtonBy);

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Login(string username, string password)
        {
            UserNameField.SendKeys(username);
            PasswordField.SendKeys(password);
            SubmitButton.Click();
        }
    }
}
