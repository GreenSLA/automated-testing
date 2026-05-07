namespace RealWorldTests.Tests;

/// <summary>
/// Базовый класс для тестов, которым авторизация нужна как предусловие
/// </summary>
public class AuthBase : TestBase
{
    protected AccountData user = null!;

    [SetUp]
    public void AuthSetup()
    {
        user = AccountData.Generate();
        app.Auth.Register(user);
        app.Auth.Login(user);
    }
}
