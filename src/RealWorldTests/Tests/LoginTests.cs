namespace RealWorldTests.Tests;

[TestFixture]
public class LoginTests : TestBase
{
    [Test]
    public void LoginWithValidData()
    {
        var user = AccountData.Generate();
        app.Auth.Register(user);
        app.Auth.Login(user);

        Assert.That(
            app.Auth.IsLoggedIn(),
            Is.True,
            "После успешного логина пользователь должен быть авторизован");
    }

    [Test]
    public void LoginWithInvalidData()
    {
        var invalidUser = new AccountData("nobody", "nobody@invalid.test", "wrongpassword");
        var result = app.Auth.TryLogin(invalidUser);

        Assert.Multiple(() =>
        {
            Assert.That(
                result,
                Is.False,
                "Логин с невалидными данными должен завершиться неуспешно");
            Assert.That(
                app.Auth.IsLoggedIn(),
                Is.False,
                "После неуспешного логина пользователь не должен быть авторизован");
        });
    }
}
