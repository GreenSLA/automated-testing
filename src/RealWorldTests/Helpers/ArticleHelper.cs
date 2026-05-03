using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RealWorldTests.Models;

namespace RealWorldTests.Helpers;

public class ArticleHelper : HelperBase
{
    private string baseURL;

    public ArticleHelper(AppManager.AppManager manager, string baseURL) : base(manager)
    {
        this.baseURL = baseURL;
    }

    public void CreateNewArticle(ArticleData article)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        driver.Navigate().GoToUrl(baseURL + "/editor");

        FillField(wait, By.XPath("//input[@placeholder='Article Title']"), article.Title);

        if (article.Description != null)
        {
            FillField(wait, By.XPath("//input[@placeholder=\"What's this article about?\"]"), article.Description);
        }

        if (article.Body != null)
        {
            FillField(wait, By.XPath("//textarea[@placeholder='Write your article (in markdown)']"), article.Body);
        }

        if (article.Tags != null)
        {
            FillField(wait, By.XPath("//input[@placeholder='Enter tags']"), article.Tags);
        }

        wait.Until(d => d.FindElement(By.XPath("//button[contains(text(),'Publish Article')]"))).Click();

        wait.Until(d => d.Url.Contains("/article/"));
    }

    public string GetArticleTitle()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        return wait.Until(d => d.FindElement(By.TagName("h1"))).Text;
    }

    public void DeleteArticle()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        wait.Until(d => d.FindElement(By.XPath("//button[contains(text(),'Delete Article')]"))).Click();
        wait.Until(d => !d.Url.Contains("/article/"));
    }

    private void FillField(WebDriverWait wait, By locator, string value)
    {
        var field = wait.Until(d => d.FindElement(locator));
        field.Clear();
        field.SendKeys(value);
    }
}
