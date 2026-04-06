namespace NaveenAutomationPOM.DataFactory.CheckoutData
{
    using NaveenAutomationPOM.DataObjects.Checkout;
    using NaveenAutomationPOM.DataFactory.RegisterData;
    using NaveenAutomationPOM.DataFactory.LoginData;

    public static class CheckoutData
    {
        public static Checkout GetGuestCheckout()
        {
            return new Checkout
            {
                CheckoutType = CheckoutType.Guest,

                BillingAddressDetails = GetDynamicBillingAddress(),

                DeliveryMethod = new DeliveryMethod
                {
                    Method = "Flat Shipping Rate",
                    Comments = $"Auto comment {Guid.NewGuid()}"
                },

                PaymentMethod = new PaymentMethod
                {
                    Method = "Cash On Delivery",
                    AcceptPrivacyPolicy = true
                }
            };
        }

        public static Checkout GetRegisterCheckout()
        {
            return new Checkout
            {
                CheckoutType = CheckoutType.Register,

                RegisterData = RegisterData.GenerateValidRegisterData(),

                BillingAddressDetails = GetDynamicBillingAddress(),

                DeliveryMethod = new DeliveryMethod
                {
                    Method = "Flat Shipping Rate",
                    Comments = $"Register flow {Guid.NewGuid()}"
                },

                PaymentMethod = new PaymentMethod
                {
                    Method = "Cash On Delivery",
                    AcceptPrivacyPolicy = true
                }
            };
        }

        public static Checkout GetReturningCheckout()
        {
            return new Checkout
            {
                CheckoutType = CheckoutType.Returning,

                Login = LoginData.GetLoginWithValidCredentials(), 

                BillingAddressDetails = GetDynamicBillingAddress(),

                DeliveryMethod = new DeliveryMethod
                {
                    Method = "Flat Shipping Rate",
                    Comments = $"Returning flow {Guid.NewGuid()}"
                },

                PaymentMethod = new PaymentMethod
                {
                    Method = "Cash On Delivery",
                    AcceptPrivacyPolicy = true
                }
            };
        }

        //  COMMON DYNAMIC BUILDER
        private static BillingAddressDetails GetDynamicBillingAddress()
        {
            return new BillingAddressDetails
            {
                FirstName = "User" + new Random().Next(1000, 9999),
                LastName = "Test" + new Random().Next(1000, 9999),
                Company = "QA Company",
                Address1 = $"Street {new Random().Next(1, 999)}",
                Address2 = $"Area {Guid.NewGuid().ToString().Substring(0, 5)}",
                City = "Ahmedabad",
                PostCode = $"{new Random().Next(100000, 999999)}",
                Country = "India",
                State = "Gujarat"
            };
        }
    }
}
