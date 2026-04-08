using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace RealWorldTests;

[TestFixture]
public class GeneratedByKatalonTest
{
    private IWebDriver driver;
    private StringBuilder verificationErrors;
    private string baseURL;
    private bool acceptNextAlert = true;

    [SetUp]
    public void SetupTest()
    {
        var options = new ChromeOptions();
        if (Environment.GetEnvironmentVariable("CI") == "true")
        {
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
        }
        driver = new ChromeDriver(options);
        baseURL = "https://www.google.com/";
        verificationErrors = new StringBuilder();
    }

    [TearDown]
    public void TeardownTest()
    {
        try
        {
            driver.Quit();
        }
        catch (Exception)
        {
        }
        finally
        {
            driver.Dispose();
        }

        Assert.AreEqual("", verificationErrors.ToString());
    }

    [Test]
    public void TheDemoAppLoginTest()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        driver.Navigate().GoToUrl("https://demo.realworld.show");
        driver.Manage().Cookies.DeleteAllCookies();
        ((IJavaScriptExecutor)driver).ExecuteScript("localStorage.clear(); sessionStorage.clear();");

        driver.Navigate().GoToUrl("https://demo.realworld.show/login");
        wait.Until(d => d.FindElement(By.LinkText("Sign in"))).Click();

        var email = wait.Until(d => d.FindElement(By.Name("email")));
        email.Clear();
        email.SendKeys("oleg@mail.com");

        var password = wait.Until(d => d.FindElement(By.Name("password")));
        password.Clear();
        password.SendKeys("oleg");

        wait.Until(d => d.FindElement(By.XPath("//button[@type='submit']"))).Click();
    }

    private bool IsElementPresent(By by)
    {
        try
        {
            driver.FindElement(by);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    private bool IsAlertPresent()
    {
        try
        {
            driver.SwitchTo().Alert();
            return true;
        }
        catch (NoAlertPresentException)
        {
            return false;
        }
    }

    private string CloseAlertAndGetItsText()
    {
        try
        {
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            if (acceptNextAlert)
            {
                alert.Accept();
            }
            else
            {
                alert.Dismiss();
            }

            return alertText;
        }
        finally
        {
            acceptNextAlert = true;
        }
    }
}