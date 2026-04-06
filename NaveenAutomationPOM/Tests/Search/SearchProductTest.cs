using NaveenAutomationPOM.DataFactory.ProductData;
using NaveenAutomationPOM.PageObjects;

namespace NaveenAutomationPOM.Tests.Search
{
    [TestClass]
    public class SearchProductTest : BaseTest
    {
        private HomePO Home => new(driver);

        [TestMethod]
        public void VerifySearchReturnsValidProductResults()
        {
            var products = ProductData.GetValidProductNames();
            var searchProduct = products[new Random().Next(products.Count)];

            log.Info("Step 1: Login to the application");
            LoginToApplication();

            log.Info($"Step 2: Search for valid product '{searchProduct}'");
            Home.SearchProduct(searchProduct);

            log.Info($"Step 3: Verify search page title contains '{searchProduct}'");
            Assert.IsTrue(Home.IsSearchResultsHeaderDisplayed(), $"Search page title mismatch for product '{searchProduct}'");

            log.Info($"Step 4: Verify product '{searchProduct}' is displayed in results");
            Assert.IsTrue(Home.IsProductDisplayed(searchProduct), $"Product '{searchProduct}' was not displayed in search results");
        }

        [TestMethod]
        public void VerifySearchReturnsNoResultsForInvalidProduct()
        {
            var searchProduct = ProductData.GetInvalidProductName();

            log.Info("Step 1: Login to the application");
            LoginToApplication();

            log.Info($"Step 2: Search for invalid product '{searchProduct}'");
            Home.SearchProduct(searchProduct);

            log.Info($"Step 3: Verify search page title contains '{searchProduct}'");
            Assert.IsTrue(Home.IsSearchResultsHeaderDisplayed(), $"Search page title mismatch for product '{searchProduct}'");

            log.Info("Step 4: Verify 'No results found' message is displayed");
            Assert.IsTrue(Home.HasNoSearchResults(), "Expected 'No product found' message was not displayed");
        }

        [TestMethod]
        public void VerifySearchReturnsNoResultsForEmptyInput()
        {
            log.Info("Step 1: Login to the application");
            LoginToApplication();

            log.Info("Step 2: Search with empty input");
            Home.SearchProduct("");

            log.Info("Step 3: Verify search page is displayed");
            Assert.AreEqual("Search", Home.GetCurrentPageText(), "Search page was not displayed");

            log.Info("Step 4: Verify 'No results found' message is displayed for empty input");
            Assert.IsTrue(Home.HasNoSearchResults(), "Search results page did not show expected behavior for empty input");
        }
    }
}