using OpenQA.Selenium;

namespace NaveenAutomationPOM.PageObjects
{
    public class CartPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By ShoppingCartLink = By.XPath("//a[contains(@href,'route=checkout/cart')]");
        private readonly By CartPageTitle = By.XPath("//div[@id='content']//h1");
        private readonly By OutOfStockProductRows = By.XPath("//td//span[contains(text(),'***')]");
        private readonly By RemoveButtonForOutOfStock = By.XPath("//td//span[contains(text(),'***')]/ancestor::tr//button[@data-original-title='Remove']");
        private readonly By RemoveButtons = By.XPath("//div[@class='table-responsive']//button[contains(@class,'btn-danger')]");
        private readonly By CartProductNames = By.XPath("//table//tbody//tr//td[2]/a");
        private static By EmptyCartMessage => By.XPath("//div[@id='content']//p[contains(text(),'Your shopping cart is empty')]");

        public void NavigateToCartPage()
        {
            actions.Click(ShoppingCartLink);
            actions.IsDisplayed(CartPageTitle);
        }

        public List<string> GetCartProductNames()
        {
            var productElements = driver.FindElements(CartProductNames);

            return [.. productElements.Select(e => e.Text.Trim())];
        }
        public bool IsCartEmptyDisplayed()
        {
            return actions.IsDisplayed(EmptyCartMessage);
        }
        public void RemoveOutOfStockProducts()
        {
            while (driver.FindElements(OutOfStockProductRows).Count > 0)
            {
                try
                {
                    var removeButton = wait.Until(d =>
                    {
                        var buttons = d.FindElements(RemoveButtonForOutOfStock);
                        return buttons.Count > 0 ? buttons[0] : null;
                    });

                    removeButton.Click();

                    actions.WaitForPageLoad();
                    wait.Until(d => d.FindElements(OutOfStockProductRows).Count >= 0);
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("Something went wrong while removing a product");
                }
            }
        }
        public void ClearCart()
        {
            while (driver.FindElements(RemoveButtons).Count > 0)
            {
                try
                {
                    var removeButton = driver.FindElements(RemoveButtons)[0];
                    removeButton.Click();
                    actions.WaitForPageLoad();
                }
                catch
                {
                    Console.WriteLine("Something went wrong, trying again...");
                }
            }
            Console.WriteLine("All products have been removed from the cart.");
        }

    }
}
