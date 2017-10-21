using OpenQA.Selenium;

namespace TestsLibrary.Pages
{
    public class AddToFavoritesPopUp
    {
        private IWebDriver _driver;

        public By existingFoldersListBy = By.XPath(@"//select[contains(@name, ""addToMyCollections$cmbExistingCollection"")]");
        public By addButtonBy = By.XPath(@"//input[contains(@value,""Add Item(s)"")]");
        public By messageCancelButtonBy = By.XPath(@"//input[contains(@id,""btnCancelAddToMyCollectionsMessage"")]");

        private IWebElement existingFoldersList;
        private IWebElement addButton;
        private IWebElement messageCancelButton;


        public AddToFavoritesPopUp(IWebDriver driver)
        {
            _driver = driver;
            existingFoldersList = _driver.FindElement(existingFoldersListBy);
            addButton = _driver.FindElement(addButtonBy);
            messageCancelButton = _driver.FindElement(messageCancelButtonBy);
        }

        public void AddToFolder(string folder)
        {
            existingFoldersList.FindElement(By.XPath($@"//option[text()=""{folder}""]")).Click();
            addButton.Click();
        }

        public void CloseMessage()
        {
            messageCancelButton.Click();
        }

    }
}
