using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace RealWorldTests;

public class TestBase
{
    protected IWebDriver Driver = null!;
    protected WebDriverWait Wait = null!;
    protected const string BaseUrl = "https://demo.realworld.show";

    protected static string TestEmail => AssemblySetup.TestEmail;
    protected static string TestPassword => AssemblySetup.TestPassword;
    protected static string TestUsername => AssemblySetup.TestUsername;

    [SetUp]
    public void SetUp()
    {
        new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

        var options = new ChromeOptions();
        // В CI (GitHub Actions) переменная CI=true, то запускаем headless автоматически
        if (Environment.GetEnvironmentVariable("CI") == "true")
            options.AddArgument("--headless=new");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        Driver = new ChromeDriver(options);
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));

        Login();
    }

    [TearDown]
    public void TearDown()
    {
        Driver?.Quit();
        Driver?.Dispose();
    }

    protected void Login()
    {
        Driver.Navigate().GoToUrl($"{BaseUrl}/login");

        Wait.Until(d => d.FindElement(By.CssSelector("input[placeholder='Email']"))).SendKeys(TestEmail);
        Driver.FindElement(By.CssSelector("input[placeholder='Password']")).SendKeys(TestPassword);
        Driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        // После входа Angular перенаправляет на главную, ждём уход со страницы логина
        Wait.Until(d => !d.Url.Contains("/login"));
    }
}