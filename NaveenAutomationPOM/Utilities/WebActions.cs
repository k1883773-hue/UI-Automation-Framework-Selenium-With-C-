using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace NaveenAutomationPOM.Utilities
{
    public class WebActions(IWebDriver driver, WebDriverWait wait)
    {
        private readonly IWebDriver driver = driver;
        private readonly WebDriverWait wait = wait;

        // Click action
        public void Click(By locator)
        {
            wait.Until(d => d.FindElement(locator).Displayed);
            driver.FindElement(locator).Click(); 
        }

        // Type text into input field
        public void Type(By locator, string text)
        {
            var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            element.Clear(); // remove old text
            element.SendKeys(text);
        }

        // Get text from element
        public string GetText(By locator)
        {
            var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element.Text;
        }

        // Check if element is displayed
        public bool IsDisplayed(By locator)
        {
            try
            {
                return wait.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
            }
            catch
            {
                return false;
            }
        }

        // Get multiple elements (useful for lists)
        public IList<IWebElement> GetElements(By locator)
        {
            return wait.Until(d => d.FindElements(locator));
        }

        //Get Attribute
        public string GetAttribute(By locator, string attributeName)
        {
            var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element.GetAttribute(attributeName);
        }

        //Page Load
        public void WaitForPageLoad()
        {
            wait.Until(driver =>
                ((IJavaScriptExecutor)driver)
                    .ExecuteScript("return document.readyState")
                    .Equals("complete"));
        }

        //DropDownSelectByText
        public void DropDownSelectByText(By locator, string text)
        {
            var element = wait.Until(d => d.FindElement(locator));
            var select = new SelectElement(element);
            select.SelectByText(text);
        }
        // Clear text
        public void Clear(IWebElement element)
        {
            wait.Until(d => element.Displayed && element.Enabled);
            element.Clear();
        }
    }
}