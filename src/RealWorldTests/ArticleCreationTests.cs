using OpenQA.Selenium;

namespace RealWorldTests;

[TestFixture]
public class ArticleCreationTests : TestBase
{
    [Test]
    public void TheCreateArticleTest()
    {
        AccountData user = AccountData.Generate();
        ArticleData article = new ArticleData("Test Article Title")
        {
            Description = "Test article description",
            Body = "This is the body of the test article.",
            Tags = "test"
        };

        OpenHomePage();
        RegisterUser(user);
        Login(user);
        CreateNewArticle(article);
    }

    public void CreateNewArticle(ArticleData article)
    {
        driver.Navigate().GoToUrl(baseURL + "/editor");

        FillField(By.XPath("//input[@placeholder='Article Title']"), article.Title);

        if (article.Description != null)
        {
            FillField(By.XPath("//input[@placeholder=\"What's this article about?\"]"), article.Description);
        }

        if (article.Body != null)
        {
            FillField(By.XPath("//textarea[@placeholder='Write your article (in markdown)']"), article.Body);
        }

        if (article.Tags != null)
        {
            FillField(By.XPath("//input[@placeholder='Enter tags']"), article.Tags);
        }

        wait.Until(d => d.FindElement(By.XPath("//button[contains(text(),'Publish Article')]"))).Click();

        wait.Until(d => d.Url.Contains("/article/"));
    }
}
