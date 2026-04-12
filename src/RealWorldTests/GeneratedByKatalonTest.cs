using OpenQA.Selenium;

namespace RealWorldTests;

[TestFixture]
public class GeneratedByKatalonTest : TestBase
{
    [Test]
    public void TheDemoAppLoginTest()
    {
        AccountData user = AccountData.Generate();

        OpenHomePage();
        RegisterUser(user);
        Login(user);
    }
}
