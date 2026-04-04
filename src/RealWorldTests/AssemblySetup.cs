using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace RealWorldTests;

[SetUpFixture]
public class AssemblySetup
{
    public static readonly string TestEmail = $"u{DateTime.Now:MMddHHmmss}@mailtest.com";
    public static readonly string TestPassword = "Test1234!";
    public static readonly string TestUsername = $"u{DateTime.Now:MMddHHmmss}";

    private const string BaseUrl = "https://demo.realworld.show";

    [OneTimeSetUp]
    public void RegisterTestAccount()
    {
        new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

        var options = new ChromeOptions();
        if (Environment.GetEnvironmentVariable("CI") == "true")
            options.AddArgument("--headless=new");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        using var driver = new ChromeDriver(options);
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        driver.Navigate().GoToUrl($"{BaseUrl}/register");

        wait.Until(d => d.FindElement(By.CssSelector("input[placeholder='Username']")))
            .SendKeys(TestUsername);
        driver.FindElement(By.CssSelector("input[placeholder='Email']")).SendKeys(TestEmail);
        driver.FindElement(By.CssSelector("input[placeholder='Password']")).SendKeys(TestPassword);
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        // Ждём пока Angular уйдёт со страницы регистрации
        wait.Until(d => !d.Url.Contains("/register"));
    }
}