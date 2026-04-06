using NaveenAutomationPOM.PageObjects;

namespace NaveenAutomationPOM.Tests.Product
{
    [TestClass]
    public class ProductTest : BaseTest
    {
        [TestMethod]
        public void VerifyHomePageProductsMatchProductPageTitles()
        {
            var home = new HomePO(driver);
            var productPO = new ProductPO(driver);

            log.Info("Step 1: Login to the application");
            LoginToApplication();

            log.Info("Step 2: Navigate to 'Laptops & Notebooks' category");
            home.NavigateToCategory("Laptops & Notebooks");
            Assert.AreEqual("Laptops & Notebooks", home.GetCurrentPageText(), "Failed to navigate to Laptops & Notebooks page");

            log.Info("Step 3: Get all product names from Home Page");
            var homePageProducts = home.GetAllProductNames();

            log.Info("Step 4: Select each product and capture product page title");
            var productPageTitles = new List<string>();

            for (int i = 0; i < homePageProducts.Count; i++)
            {
                home.SelectProductByIndex(i);
                var productDetails = productPO.GetProductDetails();
                productPageTitles.Add(productDetails.ProductName);
                driver.Navigate().Back();
            }

            log.Info("Step 5: Verify that product names on Home Page match Product Page titles");
            CollectionAssert.AreEqual(homePageProducts, productPageTitles, "Mismatch found between Home Page products and Product Page titles");
        }
    }
}