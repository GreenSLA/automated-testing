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
    }
}
