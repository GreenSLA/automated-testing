using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RealWorldTests;

public class LoginHelper : HelperBase
{
    private string apiURL;

    public LoginHelper(AppManager manager, string apiURL) : base(manager)
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
        wait.Until(d => d.FindElement(By.LinkText("Sign in"))).Click();

        var emailField = wait.Until(d => d.FindElement(By.Name("email")));
        emailField.Clear();
        emailField.SendKeys(user.Email);

        var passwordField = wait.Until(d => d.FindElement(By.Name("password")));
        passwordField.Clear();
        passwordField.SendKeys(user.Password);

        wait.Until(d => d.FindElement(By.XPath("//button[@type='submit']"))).Click();

        wait.Until(d => !d.Url.Contains("/login"));
    }
}
