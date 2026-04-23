namespace RealWorldTests;

public class AccountData
{
    public AccountData(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public static AccountData Generate()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        return new AccountData(
            username: $"testuser{timestamp}",
            email: $"test{timestamp}@example.com",
            password: "Password123"
        );
    }
}
