using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RealWorldTests;

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
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        manager.Navigation.GoToLoginPage();

        var emailField = wait.Until(d => d.FindElement(By.Name("email")));
        emailField.Clear();
        emailField.SendKeys(user.Email);

        var passwordField = wait.Until(d => d.FindElement(By.Name("password")));
        passwordField.Clear();
        passwordField.SendKeys(user.Password);

        wait.Until(d => d.FindElement(By.XPath("//button[@type='submit']"))).Click();

        wait.Until(d => !d.Url.Contains("/login"));
    }

    public bool IsLoggedIn()
    {
        // Наличие ссылки на Settings в навбаре означает что пользователь залогинен
        return driver.FindElements(By.XPath("//a[@href='/settings']")).Count > 0;
    }

    public void Logout()
    {
        if (!IsLoggedIn()) return;

        // Переходим на страницу настроек, где находится кнопка выхода
        manager.Navigation.GoToSettingsPage();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        // Кнопка "Or click here to logout."
        wait.Until(d => d.FindElement(
            By.XPath("//button[contains(., 'logout') or contains(., 'Logout')]")
        )).Click();

        // Ждём редиректа после выхода
        wait.Until(d => !d.Url.Contains("/settings"));
    }
}
