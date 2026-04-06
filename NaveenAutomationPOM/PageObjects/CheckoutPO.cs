using NaveenAutomationPOM.DataObjects.Checkout;
using OpenQA.Selenium;
namespace NaveenAutomationPOM.PageObjects
{
    public class CheckoutPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By CheckoutButton = By.XPath("//a[text()='Checkout']");

        private readonly By GuestRadioButton = By.XPath("//input[@value='guest']");
        private readonly By ContinueAccountButton = By.Id("button-account");

        //Billing details form on checkout page
        private static By InputField(string fieldName) => By.Id($"input-payment-{fieldName}");
        //continue buttons for checkout flow
        private static By ContinueButton(string step) => By.Id($"button-{step}");

        private readonly By AgreeCheckbox = By.XPath("//input[@name='agree']");
        private readonly By OrderSuccessMesssage = By.XPath("//h1[text()='Your order has been placed!']");

        public void NavigateToCheckoutPage()
        {
            actions.Click(CheckoutButton);
        }

        public void SelectGuestCheckout()
        {
            actions.Click(GuestRadioButton);
            actions.Click(ContinueAccountButton);
        }

        public void FillBillingAddressForm(BillingAddressDetails data)
        {
            actions.Type(InputField("firstname"), data.FirstName);
            actions.Type(InputField("lastname"), data.LastName);
            actions.Type(InputField("address-1"), data.Address1);
            actions.Type(InputField("city"), data.City);
            actions.Type(InputField("postcode"), data.PostCode);
            actions.DropDownSelectByText(InputField("country"), data.Country);
            actions.DropDownSelectByText(InputField("zone"), data.State);
        }

        public void CompleteRegisteredCheckout(Checkout? data = null)
        {
            // Guest flow
            if (data != null && data.CheckoutType == CheckoutType.Guest)
            {
                SelectGuestCheckout();
                FillBillingAddressForm(data.BillingAddressDetails);
            }

            // Logged-in user flow (no data needed)
            actions.Click(ContinueButton("payment-address"));
            actions.Click(ContinueButton("shipping-address"));
            actions.Click(ContinueButton("shipping-method"));
            actions.Click(AgreeCheckbox);
            actions.Click(ContinueButton("payment-method"));
            actions.Click(ContinueButton("confirm"));
        }
        public bool IsOrderSuccessMessageDisplayed()
        {
            return actions.IsDisplayed(OrderSuccessMesssage);
        }
    }
}
