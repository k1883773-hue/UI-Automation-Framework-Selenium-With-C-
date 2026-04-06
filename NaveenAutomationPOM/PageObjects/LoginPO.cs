using OpenQA.Selenium;

namespace NaveenAutomationPOM.PageObjects
{
    public class LoginPO(IWebDriver driver) : BasePO(driver)
    {
        private static By Email => By.Id("input-email");
        private static By Password => By.Id("input-password");
        private static By LoginButton => By.XPath("//input[@value='Login']");
        private static By WarningMessage => By.CssSelector(".alert-danger");

        public void LoginWithCredentials(string email, string password)
        {
            actions.Type(Email, email);
            actions.Type(Password, password);
            actions.Click(LoginButton);
        }

        public void SubmitEmptyLoginForm()
        {
            actions.Click(LoginButton);
        }

        public bool IsLoginWarningMessageDisplayed()
        {
            string message = actions.GetText(WarningMessage);

            return message.Contains("No match") ||
                   message.Contains("exceeded allowed number");
        }
    }
}
