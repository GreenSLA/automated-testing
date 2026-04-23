using NUnit.Framework;

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

        app.Navigation.OpenHomePage();
        app.Auth.Register(user);
        app.Auth.Login(user);
        app.Article.CreateNewArticle(article);

        Assert.That(app.Driver.Url, Does.Contain("/article/"), "После создания статьи должен быть переход на страницу статьи");
        Assert.That(app.Article.GetArticleTitle(), Is.EqualTo(article.Title), "Заголовок созданной статьи должен совпадать");
    }
}
