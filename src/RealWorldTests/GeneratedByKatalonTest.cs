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
    }
}
