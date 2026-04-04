using NUnit.Framework;
using OpenQA.Selenium;

namespace RealWorldTests;

[TestFixture]
[Order(1)]
public class ArticleTests : TestBase
{
    private const string ArticleTitle = "Selenium Test";
    private const string ArticleAbout = "Automated testing with Selenium";
    private const string ArticleBody  = "This article was created by an automated Selenium test.";

    [Test]
    [Order(1)]
    public void CreateArticle_WithValidData_ArticleAppearsOnPage()
    {
        Driver.Navigate().GoToUrl($"{BaseUrl}/editor");

        // Ждём загрузки формы
        Wait.Until(d => d.FindElement(By.CssSelector("input[placeholder='Article Title']"))).SendKeys(ArticleTitle);

        Driver.FindElement(By.CssSelector("input[placeholder=\"What's this article about?\"]")).SendKeys(ArticleAbout);

        Driver.FindElement(By.CssSelector("textarea[placeholder='Write your article (in markdown)']")).SendKeys(ArticleBody);

        // Кнопка публикации
        Driver.FindElement(By.XPath("//button[contains(text(),'Publish Article')]")).Click();

        // После публикации редиректит на /article/:slug
        Wait.Until(d => d.Url.Contains("/article/"));

        var heading = Wait.Until(d => d.FindElement(By.CssSelector(".banner h1")));
        Assert.That(
            heading.Text,
            Is.EqualTo(ArticleTitle),
            "Заголовок опубликованной статьи должен совпадать");
    }

    [Test]
    [Order(2)]
    public void EditArticle_ChangesTitle_TitleUpdatedOnPage()
    {
        NavigateToOwnArticle();

        // Кнопка редактирования в баннере
        Wait.Until(d => d.FindElement(By.CssSelector(".banner a.btn-outline-secondary"))).Click();

        // Ждём загрузки редактора
        var titleInput = Wait.Until(d => d.FindElement(By.CssSelector("input[placeholder='Article Title']")));
        titleInput.Clear();
        titleInput.SendKeys("Updated: " + ArticleTitle);

        Driver.FindElement(By.XPath("//button[contains(text(),'Publish Article')]")).Click();

        // Ждём обновления заголовка
        Wait.Until(d => d.FindElement(By.CssSelector(".banner h1")).Text.Contains("Updated:"));

        Assert.That(
            Driver.FindElement(By.CssSelector(".banner h1")).Text,
            Does.Contain("Updated:"),
            "Заголовок статьи должен быть обновлён");
    }

    [Test]
    [Order(3)]
    public void DeleteArticle_ArticleDeleted_RedirectsToHome()
    {
        NavigateToOwnArticle();

        // Кнопка удаления в баннере
        Wait.Until(d => d.FindElement(By.CssSelector(".banner button.btn-outline-danger"))).Click();

        // После удаления редирект на главную
        Wait.Until(d => d.Url is $"{BaseUrl}/" or BaseUrl);

        Assert.That(
            Driver.Url,
            Does.StartWith(BaseUrl),
            "После удаления статьи должен быть редирект на главную");
    }

    private void NavigateToOwnArticle()
    {
        Driver.Navigate().GoToUrl($"{BaseUrl}/profile/{TestUsername}");

        var articleLink = Wait.Until(d => d.FindElement(By.CssSelector("div.article-preview a.preview-link")));
        articleLink.Click();

        Wait.Until(d => d.Url.Contains("/article/"));
    }
}
