using OpenQA.Selenium;

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
        driver.Navigate().GoToUrl(baseURL);
        driver.Manage().Cookies.DeleteAllCookies();
        ((IJavaScriptExecutor)driver).ExecuteScript("localStorage.clear(); sessionStorage.clear();");
    }

    public void GoToLoginPage()
    {
        driver.Navigate().GoToUrl(baseURL + "/login");
    }
}
