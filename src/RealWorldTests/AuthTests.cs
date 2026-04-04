using NUnit.Framework;
using OpenQA.Selenium;

namespace RealWorldTests;

[TestFixture]
public class AuthTests : TestBase
{
    [Test]
    public void Login_WithValidCredentials_ShowsUsernameInNav()
    {
        var usernameLink = Wait.Until(d => d.FindElement(By.XPath($"//a[contains(text(),'{TestUsername}')]")));
        Assert.That(
            usernameLink.Displayed,
            Is.True,
            "После входа username должен отображаться в навигации");
    }

    [Test]
    public void Login_WithInvalidCredentials_ShowsErrors()
    {
        // Очищаем токен из localStorage, иначе Angular редиректит авторизованных со страницы /login
        ((IJavaScriptExecutor)Driver).ExecuteScript("localStorage.clear();");
        Driver.Navigate().GoToUrl($"{BaseUrl}/login");

        // Вводим неверные данные
        var emailInput = Wait.Until(d => d.FindElement(By.CssSelector("input[placeholder='Email']")));
        emailInput.Clear();
        emailInput.SendKeys("nonexistent@wrong.com");

        var passwordInput = Driver.FindElement(By.CssSelector("input[placeholder='Password']"));
        passwordInput.Clear();
        passwordInput.SendKeys("wrongpassword");

        Driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        // Ждём конкретный li внутри ul.error-messages — сам ul всегда в DOM, но пустой до ответа сервера
        var errorItem = Wait.Until(d => d.FindElement(By.CssSelector("ul.error-messages li")));
        Assert.That(
            errorItem.Displayed,
            Is.True,
            "При неверных данных должны отображаться ошибки");
    }

    [Test]
    public void Logout_ClickingLogoutButton_ShowsSignInLink()
    {
        // Переходим в настройки, там есть кнопка выхода
        Driver.Navigate().GoToUrl($"{BaseUrl}/settings");

        // Ждём кнопку "Or click here to logout."
        var logoutBtn = Wait.Until(d => d.FindElement(By.CssSelector("button.btn-outline-danger")));
        logoutBtn.Click();

        // После выхода в навигации появляется "Sign in"
        var signInLink = Wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Sign in')]")));
        Assert.That(
            signInLink.Displayed,
            Is.True,
            "После выхода должна появиться ссылка Sign in");
    }
}