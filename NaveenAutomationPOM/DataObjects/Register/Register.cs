namespace NaveenAutomationPOM.DataObjects.Register
{
    public class Register
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Optional fields
        public bool SubscribeNewsletter { get; set; }
        public bool AcceptPrivacyPolicy { get; set; }
    }
}