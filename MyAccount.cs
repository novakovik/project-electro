using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace FinalAutomation
{
    public class MyAccount
    {
        public IWebDriver driver;

        public MyAccount(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void LoginAccount(string usernameOrEmail, string password)
        {
            IWebElement myAccountLink = driver.FindElement(By.CssSelector("#menu-item-5294 > a"));
            myAccountLink.Click();

            IWebElement accountUsernameInput = driver.FindElement(By.Id("username"));
            accountUsernameInput.SendKeys(usernameOrEmail);

            IWebElement accountPasswordInput = driver.FindElement(By.Id("password"));
            accountPasswordInput.SendKeys(password);

            IWebElement loginButton = driver.FindElement(By.CssSelector("button[name=\"login\"]"));
            loginButton.Click();
        }

        public void ChangeDetails(string firstName, string lastName, string displayName)
        {

            IWebElement firstNameInput = driver.FindElement(By.Id("account_first_name"));
            firstNameInput.Clear();
            firstNameInput.SendKeys(firstName);

            IWebElement lastNameInput = driver.FindElement(By.Id("account_last_name"));
            lastNameInput.Clear();
            lastNameInput.SendKeys(lastName);

            IWebElement displayNameInput = driver.FindElement(By.Id("account_display_name"));
            displayNameInput.Clear();
            displayNameInput.SendKeys(displayName);

        }

        public void ChangePassword(string currentPassword, string newPassword, string confirmNewPassword)
        {
            IWebElement currentPasswordInput = driver.FindElement(By.Id("password_current"));
            currentPasswordInput.SendKeys(currentPassword);

            IWebElement newPasswordInput = driver.FindElement(By.Id("password_1"));
            newPasswordInput.SendKeys(newPassword);

            IWebElement confirmNewPasswordInput = driver.FindElement(By.Id("password_2"));
            confirmNewPasswordInput.SendKeys(confirmNewPassword);

            
        }

        public void SaveChanges()
        {
            IWebElement saveChangesButton = driver.FindElement(By.CssSelector("button[name=\"save_account_details\"]"));
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView()", saveChangesButton);

            Thread.Sleep(2000);   //Find better solution for the thread sleep
            saveChangesButton.Click();
        }

        public void Logout()
        {
            IWebElement logoutLink = driver.FindElement(By.CssSelector("li.woocommerce-MyAccount-navigation-link--customer-logout > a"));
            logoutLink.Click();
        }

        public string GenerateRandomPassword(int length)
        {
            const string passwordCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!.,?{}[][";
            Random random = new Random();

            string randomPassword = new string(Enumerable.Repeat(passwordCharacters, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomPassword;
        }

    }
}


