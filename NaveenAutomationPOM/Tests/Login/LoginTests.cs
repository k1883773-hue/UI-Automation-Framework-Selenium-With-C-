using NaveenAutomationPOM.DataFactory.LoginData;
using NaveenAutomationPOM.DataObjects.Login;
using NaveenAutomationPOM.PageObjects;
using NaveenAutomationPOM.Utilities;

namespace NaveenAutomationPOM.Tests.Login

{
    [TestClass]
    public class LoginTests : BaseTest
    {
        private HomePO Home => new(driver);
        private LoginPO Login => new(driver);

        [TestMethod]
        public void VerifyUserCanLoginWithValidCredentials()
        {
            var data = JsonHelper.ReadJson<LoginTestData>("TestData/LoginData.json");
            var email = data.ValidLogin.Email;
            var password = data.ValidLogin.Password;

            log.Info("Step 1: Navigate to Login Page");
            Home.SelectAccountDropdownOption("Login");
            Assert.AreEqual("Login", Home.GetCurrentPageText(), "Failed to navigate to the Login page");

            log.Info("Step 2: Login with valid credentials");
            Login.LoginWithCredentials(email, password);

            log.Info("Step 3: Verify that user is successfully logged in");
            Assert.AreEqual("Account", Home.GetCurrentPageText(), "Login failed; Account page is not displayed");
        }

        [TestMethod]
        public void VerifyLoginFailsWithInvalidCredentials()
        {
            var invalidUser = LoginData.GetLoginWithInvalidCredentials();

            log.Info("Step 1: Navigate to Login Page");
            Home.SelectAccountDropdownOption("Login");
            Assert.AreEqual("Login", Home.GetCurrentPageText(), "Failed to navigate to the Login page");

            log.Info("Step 2: Attempt login with invalid email or password");
            Login.LoginWithCredentials(invalidUser.Email, invalidUser.Password);

            log.Info("Step 3: Verify warning message is displayed for invalid login attempt");
            Assert.IsTrue(Login.IsLoginWarningMessageDisplayed(), "Expected warning message is not displayed for invalid login attempt");
        }

        [TestMethod]
        public void VerifyMandatoryFieldValidationForLogin()
        {
            log.Info("Step 1: Navigate to Login Page");
            Home.SelectAccountDropdownOption("Login");
            Assert.AreEqual("Login", Home.GetCurrentPageText(), "Failed to navigate to the Login page");

            log.Info("Step 2: Submit login form without entering any credentials");
            Login.SubmitEmptyLoginForm();

            log.Info("Step 3: Verify warning message for mandatory fields");
            Assert.IsTrue(Login.IsLoginWarningMessageDisplayed(), "Expected mandatory field warning message is not displayed");
        }

        [TestMethod]
        public void VerifyErrorMessageForInvalidEmail()
        {
            var invalidUser = LoginData.GetLoginWithInvalidCredentials();

            log.Info("Step 1: Navigate to Login Page");
            Home.SelectAccountDropdownOption("Login");
            Assert.AreEqual("Login", Home.GetCurrentPageText(), "Failed to navigate to the Login page");

            log.Info("Step 2: Attempt login with invalid email and valid password");
            Login.LoginWithCredentials(invalidUser.Email, invalidUser.Password);

            log.Info("Step 3: Verify warning message for invalid email");
            Assert.IsTrue(Login.IsLoginWarningMessageDisplayed(), "Expected warning message is not displayed for invalid email");
        }

        [TestMethod]
        public void VerifyErrorMessageForInvalidPassword()
        {
            var invalidUser = LoginData.GetLoginWithInvalidCredentials();

            log.Info("Step 1: Navigate to Login Page");
            Home.SelectAccountDropdownOption("Login");
            Assert.AreEqual("Login", Home.GetCurrentPageText(), "Failed to navigate to the Login page");

            log.Info("Step 2: Attempt login with valid email and invalid password");
            Login.LoginWithCredentials(invalidUser.Email, invalidUser.Password);

            log.Info("Step 3: Verify warning message for invalid password");
            Assert.IsTrue(Login.IsLoginWarningMessageDisplayed(), "Expected warning message is not displayed for invalid password");
        }
    }
}
