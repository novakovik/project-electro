namespace FinalAutomation;

using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class Tests
{
    public IWebDriver driver;
    public MyAccount myAccount;


    [SetUp]
    public void Setup()
    { 

        driver = new ChromeDriver();

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        driver.Manage().Window.Maximize();


        driver.Navigate().GoToUrl("https://electro.madrasthemes.com/");

        myAccount = new MyAccount(driver);

    }


    [Test]
    public void Login()
    {
        myAccount.LoginAccount("ivanaautomation", "brainster123!");

        IWebElement myAccountTitle = driver.FindElement(By.CssSelector("h1.entry-title"));
        Assert.That(myAccountTitle.Text, Is.EqualTo("My Account"));

        IWebElement username = driver.FindElement(By.CssSelector("div.woocommerce-MyAccount-content > p > strong"));
        Assert.That(username.Text, Is.EqualTo("ivanaautomation"));
    }

    [Test]
    public void ChangeAccountDetails()
    {
        myAccount.LoginAccount("ivanaautomation4", "brainster123!");

        IWebElement accountDetailsLink = driver.FindElement(By.CssSelector("li.woocommerce-MyAccount-navigation-link--edit-account > a"));
        accountDetailsLink.Click();

        myAccount.ChangeDetails("Ivana1!", "Novakovikj", "ivanaautomationChangeDetails");
        myAccount.SaveChanges();

        IWebElement successMessage = driver.FindElement(By.CssSelector("div.woocommerce-message[role=\"alert\"]"));
        Assert.That(successMessage.Text, Is.EqualTo("Account details changed successfully."));

        IWebElement myAccountTitle = driver.FindElement(By.CssSelector("h1.entry-title"));
        Assert.That(myAccountTitle.Text, Is.EqualTo("My Account"));

    }

    
    [Test]
    public void ChangePassword()
    {

        string currentPassword = File.ReadAllText("../../../password.txt");
        string newPassword = myAccount.GenerateRandomPassword(10);
        string confirmNewPassword = newPassword;

        myAccount.LoginAccount("ivanaautomation3", currentPassword);

        IWebElement accountDetailsLink = driver.FindElement(By.CssSelector("li.woocommerce-MyAccount-navigation-link--edit-account > a"));
        accountDetailsLink.Click();

        myAccount.ChangeDetails("Ivana1!", "Novakovikj", "ivanaautomationChangeDetails");

        myAccount.ChangePassword(currentPassword, newPassword, confirmNewPassword);

        myAccount.SaveChanges();

        File.WriteAllText("../../../password.txt", newPassword);

        //IWebElement successMessage = driver.FindElement(By.CssSelector("div.woocommerce-message[role=\"alert\"]"));
        //Assert.That(successMessage.Text, Is.EqualTo("Account details changed successfully."));

        //IWebElement myAccountTitle = driver.FindElement(By.CssSelector("h1.entry-title"));
        //Assert.That(myAccountTitle.Text, Is.EqualTo("My Account"));

        myAccount.Logout();

        myAccount.LoginAccount("ivanaautomation3", newPassword);

        IWebElement displayName = driver.FindElement(By.CssSelector("div.woocommerce-MyAccount-content > p > strong"));
        Assert.That(displayName.Text, Is.EqualTo("ivanaautomationChangeDetails"));


    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }

}
