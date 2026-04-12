using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace RealWorldTests;

[TestFixture]
public class TestBase
{
    protected IWebDriver driver;
    protected WebDriverWait wait;
    protected string baseURL;
    protected string apiURL;
    protected StringBuilder verificationErrors;

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
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        baseURL = "https://demo.realworld.show";
        apiURL = "https://api.realworld.show/api";
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

        Assert.That(verificationErrors.ToString(), Is.EqualTo(""));
    }

    public void RegisterUser(AccountData user)
    {
        using var httpClient = new HttpClient();
        var json = $"{{\"user\":{{\"username\":\"{user.Username}\",\"email\":\"{user.Email}\",\"password\":\"{user.Password}\"}}}}";
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        httpClient.PostAsync(apiURL + "/users", content).Wait();
    }

    public void OpenHomePage()
    {
        driver.Navigate().GoToUrl(baseURL);
        driver.Manage().Cookies.DeleteAllCookies();
        ((IJavaScriptExecutor)driver).ExecuteScript("localStorage.clear(); sessionStorage.clear();");
    }

    public void Login(AccountData user)
    {
        driver.Navigate().GoToUrl(baseURL + "/login");
        wait.Until(d => d.FindElement(By.LinkText("Sign in"))).Click();

        var email = wait.Until(d => d.FindElement(By.Name("email")));
        email.Clear();
        email.SendKeys(user.Email);

        var password = wait.Until(d => d.FindElement(By.Name("password")));
        password.Clear();
        password.SendKeys(user.Password);

        wait.Until(d => d.FindElement(By.XPath("//button[@type='submit']"))).Click();

        wait.Until(d => !d.Url.Contains("/login"));
    }

    public void FillField(By locator, string value)
    {
        var field = wait.Until(d => d.FindElement(locator));
        field.Clear();
        field.SendKeys(value);
    }

    protected bool IsElementPresent(By by)
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
}
