using OpenQA.Selenium;

namespace TestsLibrary.Pages
{
    public class AddToFavoritesPopUp
    {
        private IWebDriver _driver;

        public static By existingFoldersListBy = By.XPath(@"//select[contains(@name, ""cmbExistingCollection"")]");
        public static By addButtonBy = By.XPath(@"//input[contains(@value,""Add Item(s)"")]");
        public static By messageCancelButtonBy = By.XPath(@"//input[contains(@id,""btnCancelAddToMyCollectionsMessage"")]");
        public static By addTofolderRadioBy = By.XPath(@"//input[contains(@id,""rdoNewCollection"")]");
        public static By addToFolderNameFieldBy = By.XPath(@"//input[contains(@name,""txtCollectionName"")]");

        private IWebElement existingFoldersList;
        private IWebElement addButton;
        private IWebElement messageCancelButton;
        private IWebElement addTofolderRadio;
        private IWebElement addToFolderNameField;

        public AddToFavoritesPopUp(IWebDriver driver)
        {
            _driver = driver;
            existingFoldersList = _driver.FindElement(existingFoldersListBy);
            addButton = _driver.FindElement(addButtonBy);
            addTofolderRadio = _driver.FindElement(addTofolderRadioBy);
            addToFolderNameField = _driver.FindElement(addToFolderNameFieldBy);
        }

        public void AddToFolder(string folder)
        {
            existingFoldersList.FindElement(By.XPath($@"//option[text()=""{folder}""]")).Click();
            addButton.Click();
        }

        public void AddToNewFolder(string folder)
        {
            addTofolderRadio.Click();
            addToFolderNameField.SendKeys(folder);
            addButton.Click();
        }

        public void CloseMessage()
        {
            messageCancelButton = _driver.FindElement(messageCancelButtonBy);
            messageCancelButton.Click();
        }

    }
}
