using System.Xml.Serialization;
using RealWorldTests.Models;

namespace RealWorldTests.Tests;

[TestFixture]
public class ArticleCreationTests : AuthBase
{
    public static IEnumerable<ArticleData> ArticleDataFromXmlFile()
    {
        var xmlPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "articles.xml");
        var serializer = new XmlSerializer(typeof(List<ArticleData>));
        using var reader = new StreamReader(xmlPath);
        var articles = (List<ArticleData>?)serializer.Deserialize(reader);
        return articles ?? [];
    }

    [Test, TestCaseSource(nameof(ArticleDataFromXmlFile))]
    public void TheCreateArticleTest(ArticleData article)
    {
        app.Article.CreateNewArticle(article);

        Assert.Multiple(() =>
        {
            Assert.That(app.Driver.Url, Does.Contain("/article/"), "После создания статьи должен быть переход на страницу статьи");
            Assert.That(app.Article.GetArticleTitle(), Is.EqualTo(article.Title), "Заголовок созданной статьи должен совпадать");
        });
    }
}
