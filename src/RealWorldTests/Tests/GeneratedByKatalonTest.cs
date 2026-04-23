using NUnit.Framework;

namespace RealWorldTests;

[TestFixture]
public class GeneratedByKatalonTest : TestBase
{
    [Test]
    public void TheDemoAppLoginTest()
    {
        AccountData user = AccountData.Generate();

        app.Navigation.OpenHomePage();
        app.Auth.Register(user);
        app.Auth.Login(user);

        Assert.That(app.Driver.Url, Does.Not.Contain("/login"), "После логина должен быть редирект со страницы входа");
    }
}
