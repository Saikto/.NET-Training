using OpenQA.Selenium;

namespace TestsLibrary.Pages
{
    public class UserActionsToolBarPage
    {
        private IWebDriver _driver;

        public static By logOutButtonBy = By.XPath(@"//a[contains(@id, ""lnkLogout"")]");
        public static By accountActionsBy = By.XPath(@"//span[contains(@id, ""ucUserActionsToolbar_lblAccount"")]");
        public static By myAccountBy = By.XPath(@"//a[contains(@id, ""ucUserActionsToolbar_lnkMyAccount"")]");

        private IWebElement logOutButton;
        private IWebElement accountActions;
        private IWebElement myAccount;

        public UserActionsToolBarPage(IWebDriver driver)
        {
            _driver = driver;
            logOutButton = _driver.FindElement(logOutButtonBy);
        }

        public void GoToMyAccount()
        {
            accountActions = _driver.FindElement(accountActionsBy);
            accountActions.Click();
            myAccount = _driver.FindElement(myAccountBy);
            myAccount.Click();
        }

    }
}
