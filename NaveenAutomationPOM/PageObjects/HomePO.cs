using OpenQA.Selenium;

namespace NaveenAutomationPOM.PageObjects
{
    public class HomePO(IWebDriver driver) : BasePO(driver)
    {
        private static By MyAccount => By.XPath("//span[text()='My Account']");
        private static By AccountDropdownOption(string option) => By.LinkText(option);
        private static By CategoryMenu(string menuName) => By.XPath($"//a[text()='{menuName}']");
        private static By ShowAllButtonNavBar(string menuName) => By.XPath($"//a[contains(text(),'Show All {menuName}')]");
        private static By ProductNameLinks => By.XPath("//div[@class='product-thumb']//h4//a");
        private static By CurrentPageBreadcrumb => By.XPath("//ul[@class='breadcrumb']/li[last()]");  
        private static By SearchInput => By.Name("search");
        private static By SearchButton => By.CssSelector("button[type='button'][class*='btn-default']");
        private static By ProductSearchResult => By.CssSelector(".product-thumb");
        private static By NoSearchResultsMessage => By.XPath("//div[@id='content']/p[contains(text(),'There is no product that matches the search criteria.')]");
        private static By SearchResultsHeader => By.XPath("//div[@id='content']/h1");

        public void SelectAccountDropdownOption(string option)
        {
            actions.Click(MyAccount);
            actions.Click(AccountDropdownOption(option));
        }

        public void NavigateToCategory(string categoryName)
        {
            actions.Click(CategoryMenu(categoryName));
            actions.Click(ShowAllButtonNavBar(categoryName));
        }

        public string GetCurrentPageText()
        {
            return actions.GetText(CurrentPageBreadcrumb);
        }
        public string GetAccountOptionText(string option)
        {
            return actions.GetText(AccountDropdownOption(option));
        }

        public List<string> GetAllProductNames()
        {
            var elements = actions.GetElements(ProductNameLinks);
            return [.. elements.Select(e => e.Text.Trim())];
        }

        public void SelectProductByIndex(int index)
        {
            var products = actions.GetElements(ProductNameLinks);
            products[index].Click();
            actions.WaitForPageLoad();
        }
                
        public void SearchProduct(string productName)
        {
            actions.Type(SearchInput, productName);
            actions.Click(SearchButton);
        }

        public bool IsProductDisplayed(string productName)
        {
            var products = actions.GetElements(ProductSearchResult);
            return products.Any(p => p.Text.Contains(productName, StringComparison.OrdinalIgnoreCase));
        }

        public bool HasNoSearchResults()
        {
            const string expectedMessage = "There is no product that matches the search criteria";
            var elements = actions.GetElements(NoSearchResultsMessage);
            return elements.Any(e => e.Text.Contains(expectedMessage, StringComparison.OrdinalIgnoreCase));
        }
        public bool IsSearchResultsHeaderDisplayed()
        {
            return actions.IsDisplayed(SearchResultsHeader);
        }
    }
}