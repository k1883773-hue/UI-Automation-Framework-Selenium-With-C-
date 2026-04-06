using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace NaveenAutomationPOM.Utilities
{
    public class SeleniumWait
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SeleniumWait(IWebDriver driver, int timeout = 15)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
        }

        // Wait for element visible
        public IWebElement UntilElementVisible(By locator)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        // Wait for element clickable
        public IWebElement UntilElementClickable(By locator)
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        // Wait for element invisible
        public bool UntilElementInvisible(By locator)
        {
            return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        // Wait for element exists (present in DOM)
        public IWebElement UntilElementExists(By locator)
        {
            return wait.Until(d =>
            {
                var elements = d.FindElements(locator);
                return elements.Count > 0 ? elements[0] : null;
            });
        }

        // Wait for all elements
        public IList<IWebElement> UntilAllElementsLocated(By locator)
        {
            return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        // Wait for text
        public bool UntilTextPresent(By locator, string text)
        {
            return wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }

        // Wait for URL
        public bool UntilUrlContains(string partialUrl)
        {
            return wait.Until(d => d.Url.Contains(partialUrl));
        }

        // Wait for page load (ONLY ONE METHOD)
        public void UntilPageLoad()
        {
            wait.Until(d =>
                ((IJavaScriptExecutor)d)
                .ExecuteScript("return document.readyState")
                .Equals("complete"));
        }

        // Wait for attribute contains
        public bool UntilAttributeContains(By locator, string attribute, string value)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return element.GetAttribute(attribute).Contains(value);
            });
        }

        // Optional element (no exception)
        public IWebElement? FindIfExists(By locator, int timeout = 5)
        {
            try
            {
                var shortWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return shortWait.Until(d =>
                {
                    var elements = d.FindElements(locator);
                    return elements.Count > 0 ? elements[0] : null;
                });
            }
            catch
            {
                return null;
            }
        }
    }
}