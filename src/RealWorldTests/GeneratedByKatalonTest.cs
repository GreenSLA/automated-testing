using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
        driver = new ChromeDriver();
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
        driver.Navigate().GoToUrl("https://demo.realworld.show/login");
        driver.FindElement(By.LinkText("Sign in")).Click();
        driver.FindElement(By.Name("email")).Click();
        driver.FindElement(By.Name("email")).Clear();
        driver.FindElement(By.Name("email")).SendKeys("oleg@mail.com");
        driver.FindElement(By.Name("password")).Click();
        driver.FindElement(By.Name("password")).Clear();
        driver.FindElement(By.Name("password")).SendKeys("oleg");
        driver.FindElement(By.XPath("//button[@type='submit']")).Click();
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