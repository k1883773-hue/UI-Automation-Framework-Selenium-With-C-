using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NaveenAutomationPOM.Utilities;

namespace NaveenAutomationPOM.PageObjects
{
    public abstract class BasePO
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;
        protected readonly WebActions actions;

        public BasePO(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            actions = new WebActions(driver, wait);
        }
    }
}