using NUnit.Framework;

namespace RealWorldTests;

[TestFixture]
public class ArticleDeletionTests : TestBase
{
    [Test]
    public void TheDeleteArticleTest()
    {
        AccountData user = AccountData.Generate();
        ArticleData article = new ArticleData("Article To Delete")
        {
            Description = "This article will be deleted",
            Body = "Temporary content.",
            Tags = "delete"
        };

        app.Navigation.OpenHomePage();
        app.Auth.Register(user);
        app.Auth.Login(user);
        app.Article.CreateNewArticle(article);
        app.Article.DeleteArticle();

        Assert.That(app.Driver.Url, Does.Not.Contain("/article/"), "После удаления статьи должен быть редирект с её страницы");
    }
}
