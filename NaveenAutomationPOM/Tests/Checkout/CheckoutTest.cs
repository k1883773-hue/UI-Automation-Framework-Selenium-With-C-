using NaveenAutomationPOM.DataFactory.ProductData;
using NaveenAutomationPOM.PageObjects;

namespace NaveenAutomationPOM.Tests.Checkout
{
    [TestClass]
    public class CheckoutTest : BaseTest
    {
        [TestMethod]
        public void VerifyRegisteredUserCheckoutProcess()
        {
            // Page Objects
            var home = new HomePO(driver);
            var product = new ProductPO(driver);
            var cart = new CartPO(driver);
            var checkout = new CheckoutPO(driver);

            // DTO
            var productsToAdd = ProductData.GetValidProductNames();

            log.Info("Step 1: Login to the application");
            LoginToApplication();

            log.Info("Step 2: Navigate to Desktops category and add products");
            home.NavigateToCategory("Desktops");
            Assert.AreEqual("Desktops", home.GetCurrentPageText(), "Failed to navigate to Desktops category");

            var addedProducts = product.AddProductsByName(productsToAdd);

            log.Info("Step 3: Navigate to Cart page");
            cart.NavigateToCartPage();
            Assert.AreEqual("Shopping Cart", home.GetCurrentPageText(), "Failed to navigate to Cart page");

            log.Info("Step 4: Verify selected products are present in the cart");
            var cartProductNames = cart.GetCartProductNames();
            foreach (var selectedProduct in addedProducts)
            {
                Assert.Contains(name => name.Contains(selectedProduct.ProductName), cartProductNames, $"Product not found in cart: {selectedProduct.ProductName}");
            }

            log.Info("Step 5: Remove out-of-stock products from the cart");
            cart.RemoveOutOfStockProducts();

            log.Info("Step 6: Proceed to Checkout page");
            checkout.NavigateToCheckoutPage();
            Assert.AreEqual("Checkout", home.GetCurrentPageText(), "Failed to navigate to Checkout page");

            log.Info("Step 7: Complete checkout for registered user");
            checkout.CompleteRegisteredCheckout();

            log.Info("Step 8: Verify order success message is displayed");
            Assert.IsTrue(checkout.IsOrderSuccessMessageDisplayed(), "Order success message was not displayed");
        }
    }
}