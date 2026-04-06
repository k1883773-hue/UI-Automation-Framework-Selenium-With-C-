using NaveenAutomationPOM.PageObjects;

namespace NaveenAutomationPOM.Tests.Logout
{
    [TestClass]
    public class LogoutTest : BaseTest
    {
        [TestMethod]
        public void VerifyUserCanLogout()
        {
            //PO
            var home = new HomePO(driver);

            log.Info("Step 1: Verify user login to the application");
            LoginToApplication();

            log.Info("Step 2: Verify that user successfully Logout from the application");
            home.SelectAccountDropdownOption("Logout");
            Assert.AreEqual("Logout", home.GetCurrentPageText(), "User is not logged out from the application");
        }
    }
}
