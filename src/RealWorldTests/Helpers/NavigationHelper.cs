using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RealWorldTests;

public class NavigationHelper : HelperBase
{
    private string baseURL;

    public NavigationHelper(AppManager manager, string baseURL) : base(manager)
    {
        this.baseURL = baseURL;
    }

    public void GoToHomePage()
    {
        driver.Navigate().GoToUrl(baseURL);
    }

    public void OpenHomePage()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        // Сначала явно логаутимся через UI - это единственный надёжный способ сбросить
        manager.Auth.Logout();

        driver.Navigate().GoToUrl(baseURL);
        driver.Manage().Cookies.DeleteAllCookies();
        ((IJavaScriptExecutor)driver).ExecuteScript("localStorage.clear(); sessionStorage.clear();");
        driver.Navigate().Refresh();
        wait.Until(d => d.FindElements(By.TagName("app-root")).Count > 0);
    }

    public void GoToLoginPage()
    {
        driver.Navigate().GoToUrl(baseURL + "/login");
    }

    public void GoToSettingsPage()
    {
        driver.Navigate().GoToUrl(baseURL + "/settings");
    }
}
