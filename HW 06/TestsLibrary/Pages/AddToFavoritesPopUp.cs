using System;
using OpenQA.Selenium;

namespace TestsLibrary.Pages
{
    public class AddToFavoritesPopUp
    {
        private readonly IWebDriver _driver;

        public static By ExistingFoldersListBy = By.XPath(@"//select[contains(@name, ""cmbExistingCollection"")]");
        public static By AddButtonBy = By.XPath(@"//input[contains(@value,""Add Item(s)"")]");
        public static By MessageCancelButtonBy = By.XPath(@"//input[contains(@id,""btnCancelAddToMyCollectionsMessage"")]");
        public static By AddTofolderRadioBy = By.XPath(@"//input[contains(@id,""rdoNewCollection"")]");
        public static By AddToFolderNameFieldBy = By.XPath(@"//input[contains(@name,""txtCollectionName"")]");

        private IWebElement ExistingFoldersList => _driver.FindElement(ExistingFoldersListBy);
        private IWebElement AddButton => _driver.FindElement(AddButtonBy);
        private IWebElement MessageCancelButton => _driver.FindElement(MessageCancelButtonBy);
        private IWebElement AddTofolderRadio => _driver.FindElement(AddTofolderRadioBy);
        private IWebElement AddToFolderNameField => _driver.FindElement(AddToFolderNameFieldBy);

        public AddToFavoritesPopUp(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AddToFolder(string folder)
        {
            ExistingFoldersList.FindElement(By.XPath($@"//option[text()=""{folder}""]")).Click();
            AddButton.Click();
        }

        public void AddToNewFolder(string folder)
        {
            AddTofolderRadio.Click();
            AddToFolderNameField.SendKeys(folder);
            AddButton.Click();
        }

        public void CloseMessage()
        {
            MessageCancelButton.Click();
        }

        public void AddToFavoritesAnyway(string folder)
        {
            try
            {
                ExistingFoldersList.FindElement(By.XPath($@"//option[text()=""{folder}""]")).Click();
                AddButton.Click();
            }
            catch (Exception)
            {
                AddTofolderRadio.Click();
                AddToFolderNameField.SendKeys(folder);
                AddButton.Click();
            }
        }

    }
}
