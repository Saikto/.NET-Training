using OpenQA.Selenium;

namespace TestsLibrary.Pages
{
    public class UserActionsToolBarPage
    {
        private readonly IWebDriver _driver;

        public static By LogOutButtonBy = By.XPath(@"//a[contains(@id, ""lnkLogout"")]");
        public static By AccountActionsBy = By.XPath(@"//span[contains(@id, ""ucUserActionsToolbar_lblAccount"")]");
        public static By MyAccountBy = By.XPath(@"//a[contains(@id, ""ucUserActionsToolbar_lnkMyAccount"")]");

        private IWebElement LogOutButton => _driver.FindElement(LogOutButtonBy);
        private IWebElement AccountActions => _driver.FindElement(AccountActionsBy);
        private IWebElement MyAccount => _driver.FindElement(MyAccountBy);

        public UserActionsToolBarPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoToMyAccount()
        {
            AccountActions.Click();
            MyAccount.Click();
        }

        public void LogOut()
        {
            LogOutButton.Click();
        }

    }
}
