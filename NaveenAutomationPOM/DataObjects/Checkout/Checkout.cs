
namespace NaveenAutomationPOM.DataObjects.Checkout

{
    public class Checkout
    {
        public required CheckoutType CheckoutType { get; set; }
        public Register.Register? RegisterData { get; set; }
        public Login.Login? Login { get; set; }
        public required BillingAddressDetails BillingAddressDetails { get; set; }
        public required DeliveryMethod DeliveryMethod { get; set; }
        public required PaymentMethod PaymentMethod { get; set; }
    }
    public enum CheckoutType
    {
        Register,
        Guest,
        Returning
    }

    public class BillingAddressDetails
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Company { get; set; }
        public required string Address1 { get; set; }
        public required string Address2 { get; set; }
        public required string City { get; set; }
        public required string PostCode { get; set; }
        public required string Country { get; set; }
        public required string State { get; set; }
    }
    public class DeliveryMethod
    {
        public required string Method { get; set; }
        public required string Comments { get; set; }
    }
    public class PaymentMethod
    {
        public required string Method { get; set; }
        public bool AcceptPrivacyPolicy { get; set; }
    }
}
