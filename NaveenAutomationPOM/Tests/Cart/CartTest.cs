using NaveenAutomationPOM.PageObjects;
namespace NaveenAutomationPOM.Tests.Cart

{
    [TestClass]
    public class CartTest : BaseTest
    {
        [TestMethod]
        public void VerifyMultipleProductsAddedToCartSuccessfully()
        {
            //PO
            var home = new HomePO(driver);
            var product = new ProductPO(driver);
            var cart = new CartPO(driver);

            log.Info("Step 1: Login to application");
            LoginToApplication();

            log.Info("Step 2: Navigate to Desktops category");
            home.NavigateToCategory("Desktops");
            Assert.AreEqual("Desktops", home.GetCurrentPageText(), "Failed to navigate to Desktops page");

            log.Info("Step 3:  Selecting and adding random products to cart");
            var selectedProducts = product.SelectRandomProductsAndAddToCart(3);

            log.Info("Step 4: Navigate to Cart page");
            cart.NavigateToCartPage();
            Assert.AreEqual("Shopping Cart", home.GetCurrentPageText(), "Failed to navigate to Cart page");

            log.Info("Step 5: Verifying all selected products are present in the cart");
            var cartProductNames = cart.GetCartProductNames();
            foreach (var selectedProduct in selectedProducts)
            {
                Assert.Contains(name => name.Contains(selectedProduct.ProductName), cartProductNames, $"Product not found in cart: {selectedProduct.ProductName}");
            }

            log.Info("Step 6: Removing all products from the cart");
            cart.ClearCart();
            Assert.IsTrue(cart.IsCartEmptyDisplayed(), "Cart is not empty after removing all products");
        }
    }
}