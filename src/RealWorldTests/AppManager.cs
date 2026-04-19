using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RealWorldTests;

public class AppManager
{
    private IWebDriver driver;
    private StringBuilder verificationErrors;
    private string baseURL;
    private string apiURL;

    private NavigationHelper navigation;
    private LoginHelper auth;
    private ArticleHelper article;

    public AppManager()
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
        baseURL = "https://demo.realworld.show";
        apiURL = "https://api.realworld.show/api";
        verificationErrors = new StringBuilder();

        navigation = new NavigationHelper(this, baseURL);
        auth = new LoginHelper(this, apiURL);
        article = new ArticleHelper(this, baseURL);
    }

    public IWebDriver Driver => driver;
    public NavigationHelper Navigation => navigation;
    public LoginHelper Auth => auth;
    public ArticleHelper Article => article;
    public StringBuilder VerificationErrors => verificationErrors;

    public void Stop()
    {
        driver.Quit();
    }
}
