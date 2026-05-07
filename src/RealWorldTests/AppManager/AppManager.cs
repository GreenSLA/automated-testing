using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RealWorldTests.Helpers;

namespace RealWorldTests.AppManager;

public class AppManager
{
    private static ThreadLocal<AppManager> _app = new();

    private IWebDriver driver;
    private StringBuilder verificationErrors;
    private string baseURL;
    private string apiURL;

    private NavigationHelper navigation;
    private LoginHelper auth;
    private ArticleHelper article;

    private AppManager()
    {
        var options = new ChromeOptions();
        if (Environment.GetEnvironmentVariable("CI") == "true")
        {
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
        }

        driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
        baseURL = Settings.Settings.BaseUrl;
        apiURL = Settings.Settings.ApiUrl;
        verificationErrors = new StringBuilder();

        navigation = new NavigationHelper(this, baseURL);
        auth = new LoginHelper(this, apiURL);
        article = new ArticleHelper(this, baseURL);
    }

    public static AppManager GetInstance()
    {
        if (!_app.IsValueCreated)
        {
            var newInstance = new AppManager();
            newInstance.Navigation.OpenHomePage();
            _app.Value = newInstance;
        }
        return _app.Value!;
    }

    ~AppManager()
    {
        try
        {
            driver.Quit();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public IWebDriver Driver => driver;
    public NavigationHelper Navigation => navigation;
    public LoginHelper Auth => auth;
    public ArticleHelper Article => article;
    public StringBuilder VerificationErrors => verificationErrors;
}
