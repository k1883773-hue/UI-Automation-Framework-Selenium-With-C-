using NaveenAutomationPOM.DataObjects.Login;

namespace NaveenAutomationPOM.DataFactory.LoginData
{
    public static class LoginData
    {
        public static Login GetLoginWithValidCredentials()
        {
            return new Login
            {
                Email = "Test@yopmail.com",
                Password = "Admin@123"
            };
        }
        public static Login GetLoginWithInvalidCredentials()
        {
            return new Login
            {
                Email = "invalid@test.com",
                Password = "Wrong@123"
            };
        }
    }
}