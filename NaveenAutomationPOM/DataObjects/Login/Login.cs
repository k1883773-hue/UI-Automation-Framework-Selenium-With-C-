namespace NaveenAutomationPOM.DataObjects.Login
{
    public class Login
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginTestData
    {
        public required Login ValidLogin { get; set; }
        public required Login InvalidLogin { get; set; }
    }
}