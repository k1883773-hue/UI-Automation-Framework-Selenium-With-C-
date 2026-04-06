using NaveenAutomationPOM.DataObjects.Register;
using OpenQA.Selenium;

namespace NaveenAutomationPOM.PageObjects
{
    public class RegistrationPO(IWebDriver driver) : BasePO(driver)
    {
        private static By InputField(string fieldName) => By.Id($"input-{fieldName}");
        private static By PrivacyPolicy => By.Name("agree");
        private static By ContinueButton => By.XPath("//input[@value='Continue']");
        private static By SuccessHeader => By.XPath("//div[@id='content']/h1");
        private static By PrivacyWarningMessage => By.XPath("//div[contains(@class,'alert-danger')]");
        private readonly By NewsletterButton = By.XPath("//input[@name='newsletter' and @value='1']");
        private readonly By ConfirmPasswordErrorMessage = By.XPath("//input[@id='input-confirm']/following-sibling::div");

        private void FillRegistrationForm(Register user, string confirmPassword)
        {
            actions.Type(InputField("firstname"), user.FirstName);
            actions.Type(InputField("lastname"), user.LastName);
            actions.Type(InputField("email"), user.Email);
            actions.Type(InputField("telephone"), user.Telephone);
            actions.Type(InputField("password"), user.Password);
            actions.Type(InputField("confirm"), confirmPassword);

            // Newsletter (optional)
            if (user.SubscribeNewsletter)
                actions.Click(NewsletterButton);
        }
        private void SubmitRegisterForm(bool acceptPrivacyPolicy)
        {
            if (acceptPrivacyPolicy)
                actions.Click(PrivacyPolicy);

            actions.Click(ContinueButton);
        }
        public void RegisterUser(Register user)
        {
            FillRegistrationForm(user, user.Password);
            SubmitRegisterForm(user.AcceptPrivacyPolicy);
        }
        public void RegisterWithoutPrivacy(Register user)
        {
            FillRegistrationForm(user, user.Password);
            SubmitRegisterForm(false);
        }
        public void RegisterWithInvalidPassword(Register user, string confirmPassword)
        {
            FillRegistrationForm(user, confirmPassword);
            SubmitRegisterForm(user.AcceptPrivacyPolicy);
        }
        public bool GetSuccessHeaderText()
        {
            return actions.IsDisplayed(SuccessHeader);
        }

        public bool IsEmailAlreadyExistsErrorDisplayed()
        {
            return actions.IsDisplayed(PrivacyWarningMessage);
        }

        public bool GetPrivacyWarningMessage()
        {
            return actions.IsDisplayed(PrivacyWarningMessage);
        }

        public bool GetConfirmPasswordErrorMessage()
        {
            return actions.IsDisplayed(ConfirmPasswordErrorMessage);
        }

        public void SubmitEmptyRegistrationForm()
        {
            actions.Click(ContinueButton);
        }
    }
}
