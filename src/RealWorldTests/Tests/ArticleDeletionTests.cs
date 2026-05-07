using RealWorldTests.Models;

namespace RealWorldTests.Tests;

[TestFixture]
public class ArticleDeletionTests : AuthBase
{
    [Test]
    public void TheDeleteArticleTest()
    {
        ArticleData article = new ArticleData("Article To Delete")
        {
            Description = "This article will be deleted",
            Body = "Temporary content.",
            Tags = "delete"
        };

        app.Article.CreateNewArticle(article);
        app.Article.DeleteArticle();

        Assert.That(app.Driver.Url, Does.Not.Contain("/article/"), "После удаления статьи должен быть редирект с её страницы");
    }
}
