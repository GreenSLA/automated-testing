namespace RealWorldTests.Tests;

[TestFixture]
public class GeneratedByKatalonTest : TestBase
{
    [Test]
    public void TheDemoAppLoginTest()
    {
        var user = AccountData.Generate();
        app.Auth.Register(user);
        app.Auth.Login(user);

        Assert.That(
            app.Driver.Url,
            Does.Not.Contain("/login"),
            "После логина должен быть редирект со страницы входа");
    }
}
