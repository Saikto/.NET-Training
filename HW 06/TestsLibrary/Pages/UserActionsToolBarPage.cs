using OpenQA.Selenium;

namespace TestsLibrary.Pages
{
    public class UserActionsToolBarPage
    {
        private IWebDriver _driver;

        public By logOutButtonBy = By.XPath(@"//a[contains(@id, ""lnkLogout"")]");
        public By accountActionsBy = By.XPath(@"//span[contains(@id, ""ucUserActionsToolbar_lblAccount"")]");
        public By myAccountBy = By.XPath(@"//a[contains(@id, ""ucUserActionsToolbar_lnkMyAccount"")]");

        private IWebElement logOutButton;
        private IWebElement accountActions;
        private IWebElement myAccount;

        public UserActionsToolBarPage(IWebDriver driver)
        {
            _driver = driver;
            logOutButton = _driver.FindElement(logOutButtonBy);
            accountActions = _driver.FindElement(accountActionsBy);
            myAccount = _driver.FindElement(myAccountBy);
        }

        public void GoToMyAccount()
        {
            accountActions.Click();
            myAccount.Click();
        }

    }
}
