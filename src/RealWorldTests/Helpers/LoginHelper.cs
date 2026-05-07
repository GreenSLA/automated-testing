using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RealWorldTests.Helpers;

public class LoginHelper : HelperBase
{
    private string apiURL;

    public LoginHelper(AppManager.AppManager manager, string apiURL) : base(manager)
    {
        this.apiURL = apiURL;
    }

    public void Register(AccountData user)
    {
        using var httpClient = new HttpClient();
        var json = $"{{\"user\":{{\"username\":\"{user.Username}\",\"email\":\"{user.Email}\",\"password\":\"{user.Password}\"}}}}";
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        httpClient.PostAsync(apiURL + "/users", content).Wait();
    }

    public void Login(AccountData user)
    {
        if (!TryLogin(user))
            throw new WebDriverTimeoutException($"Не удалось войти под пользователем {user.Email}");
    }

    public bool TryLogin(AccountData user)
    {
        if (IsLoggedIn())
        {
            if (IsLoggedIn(user.Username))
                return true;

            Logout();
        }

        manager.Navigation.GoToLoginPage();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        var emailField = wait.Until(d => d.FindElement(By.Name("email")));
        emailField.Clear();
        emailField.SendKeys(user.Email);

        var passwordField = wait.Until(d => d.FindElement(By.Name("password")));
        passwordField.Clear();
        passwordField.SendKeys(user.Password);

        wait.Until(d => d.FindElement(By.XPath("//button[@type='submit']"))).Click();

        try
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(d => !d.Url.Contains("/login"));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    /// <summary>
    /// Проверяет, залогинен ли кто-либо
    /// </summary>
    public bool IsLoggedIn()
    {
        return driver.FindElements(By.XPath("//a[@href='/settings']")).Count > 0;
    }

    /// <summary>
    /// Проверяет, залогинен ли конкретный пользователь по username в навбаре
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    public bool IsLoggedIn(string username)
    {
        return driver.FindElements(By.XPath($"//nav//a[normalize-space()='{username}']")).Count > 0;
    }

    /// <summary>
    /// Разлогиниться
    /// </summary>
    public void Logout()
    {
        if (!IsLoggedIn()) return;

        manager.Navigation.GoToSettingsPage();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(
            By.XPath("//button[contains(., 'logout') or contains(., 'Logout')]")
        )).Click();

        wait.Until(d => !d.Url.Contains("/settings"));
    }
}
