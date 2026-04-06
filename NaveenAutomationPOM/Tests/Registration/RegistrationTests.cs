using NaveenAutomationPOM.DataFactory.RegisterData;
using NaveenAutomationPOM.PageObjects;
namespace NaveenAutomationPOM.Tests.Registration
{
    [TestClass]
    public class RegistrationTests : BaseTest
    {
        private HomePO Home => new(driver);
        private RegistrationPO Registration => new(driver);

        [TestMethod]
        public void VerifyUserCanRegisterWithValidDetails()
        {
            // DTO
            var user = RegisterData.GenerateValidRegisterData();

            log.Info("Step 1: Navigate to Registration Page");
            Home.SelectAccountDropdownOption("Register");
            Assert.AreEqual("Register", Home.GetCurrentPageText(), "Failed to navigate to the Registration page");

            log.Info("Step 2: Fill registration form with valid user data");
            Registration.RegisterUser(user);

            log.Info("Step 3: Submit registration form and verify success message");
            Assert.IsTrue(Registration.GetSuccessHeaderText(), "Registration success message mismatch");
            Assert.AreEqual("Success", Home.GetCurrentPageText(), "User is not logged in after registration");
        }

        [TestMethod]
        public void VerifyRegistrationFailsWithExistingEmail()
        {
            // DTO
            var user = RegisterData.GenerateUserWithExistingEmail();

            log.Info("Step 1: Navigate to Registration Page");
            Home.SelectAccountDropdownOption("Register");
            Assert.AreEqual("Register", Home.GetCurrentPageText(), "Failed to navigate to the Registration page");

            log.Info("Step 2: Attempt to register with an existing email");
            Registration.RegisterUser(user);

            log.Info("Step 3: Verify validation message for existing email");
            Assert.IsTrue(Registration.IsEmailAlreadyExistsErrorDisplayed(), "Expected error message for existing email is not displayed");
        }

        [TestMethod]
        public void VerifyRegistrationFailsWithoutAcceptingPrivacyPolicy()
        {
            // DTO
            var user = RegisterData.GenerateValidRegisterData();

            log.Info("Step 1: Navigate to Registration Page");
            Home.SelectAccountDropdownOption("Register");
            Assert.AreEqual("Register", Home.GetCurrentPageText(), "Failed to navigate to the Registration page");

            log.Info("Step 2: Attempt to register without accepting the privacy policy");
            Registration.RegisterWithoutPrivacy(user);

            log.Info("Step 3: Verify warning message for missing privacy policy agreement");
            Assert.IsTrue(Registration.GetPrivacyWarningMessage(), "Expected privacy policy warning message is not displayed");
        }

        [TestMethod]
        public void VerifyRegistrationFailsWithMismatchedPassword()
        {
            // DTO
            var (user, confirmPassword) = RegisterData.GenerateUserWithMismatchedPassword();

            log.Info("Step 1: Navigate to Registration Page");
            Home.SelectAccountDropdownOption("Register");
            Assert.AreEqual("Register", Home.GetCurrentPageText(), "Failed to navigate to the Registration page");

            log.Info("Step 2: Attempt to register with mismatched password and confirm password");
            Registration.RegisterWithInvalidPassword(user, confirmPassword);

            log.Info("Step 3: Verify error message for mismatched password confirmation");
            Assert.IsTrue(Registration.GetConfirmPasswordErrorMessage(), "Expected password mismatch warning message is not displayed");
        }

        [TestMethod]
        public void VerifyMandatoryFieldValidationOnRegistration()
        {
            log.Info("Step 1: Navigate to Registration Page");
            Home.SelectAccountDropdownOption("Register");
            Assert.AreEqual("Register", Home.GetCurrentPageText(), "Failed to navigate to the Registration page");

            log.Info("Step 2: Attempt to submit registration form without filling mandatory fields");
            Registration.SubmitEmptyRegistrationForm();

            log.Info("Step 3: Verify error messages for mandatory fields");
            Assert.IsTrue(Registration.IsEmailAlreadyExistsErrorDisplayed(), "Expected mandatory field error message is not displayed");
        }
    }

}
