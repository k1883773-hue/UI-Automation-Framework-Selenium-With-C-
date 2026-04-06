using NaveenAutomationPOM.DataObjects.Product;
using NaveenAutomationPOM.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProductModel = NaveenAutomationPOM.DataObjects.Product.Product;

namespace NaveenAutomationPOM.PageObjects
{
    public class ProductPO(IWebDriver driver) : BasePO(driver)
    {
        private static By ProductNameLinks => By.XPath("//div[@class='product-thumb']//h4//a");
        private readonly By ProductTitle = By.XPath("//div[@id='content']//h1");
        private readonly By ProductPrice = By.XPath("//ul[@class='list-unstyled']//h2");
        private readonly By ProductImage = By.XPath("//ul[@class='thumbnails']//img");
        private readonly By ProductDescription = By.XPath("//div[@id='tab-description']");
        private readonly By AddToCartButton = By.Id("button-cart");
        private readonly By ProductBrand = By.XPath("//li[contains(text(),'Brand')]/a");
        private readonly By ProductCode = By.XPath("//li[contains(text(),'Product Code')]");
        private readonly By CartSuccessMsg = By.CssSelector(".alert-success");
        private static By RequiredOptions => By.XPath("//div[contains(@class,'required')]");
        private static By RadioOptions => By.XPath(".//input[@type='radio']");
        private static By CheckboxOptions => By.XPath(".//input[@type='checkbox']");
        private static By SelectDropdown => By.XPath(".//select");
        private static By TextField => By.XPath(".//input[@type='text']");
        private static By TextArea => By.XPath(".//textarea");
        private static By DateField => By.XPath(".//input[contains(@class,'date')]");
        private static By ExTaxPrice => By.XPath("//ul[@class='list-unstyled']//li[contains(text(),'Ex Tax')]");

        public void AddProductToCart(string productName)
        {
            actions.Click(AddToCartButton);
            string successMsg = actions.GetText(CartSuccessMsg);

            if (!successMsg.Contains("Success") ||
                !successMsg.Contains(productName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception($"Add to cart failed for product: {productName}");
            }
        }

        public void FillMandatoryOptions()
        {
            var optionBlocks = actions.GetElements(RequiredOptions);

            if (optionBlocks.Count == 0)
            {
                Console.WriteLine("No mandatory options present");
                return;
            }

            foreach (var option in optionBlocks)
            {
                // radio field
                var radio = option.FindElements(RadioOptions).FirstOrDefault(e => e.Displayed && e.Enabled);
                if (radio != null)
                {
                    radio.Click();
                    continue;
                }

                // checkbox field
                var checkbox = option.FindElements(CheckboxOptions).FirstOrDefault(e => e.Displayed && e.Enabled);
                if (checkbox != null)
                {
                    checkbox.Click();
                    continue;
                }

                // dropdown field
                var dropdown = option.FindElements(SelectDropdown).FirstOrDefault(e => e.Displayed && e.Enabled);
                if (dropdown != null)
                {
                    var select = new SelectElement(dropdown);
                    if (select.Options.Count > 1)
                        select.SelectByIndex(1);
                    continue;
                }

                // text field (faker)
                var text = option.FindElements(TextField).FirstOrDefault(e => e.Displayed && e.Enabled);
                if (text != null)
                {
                    text.Clear();
                    text.SendKeys(FakerHelper.GetName());
                    continue;
                }

                // textarea field (faker)
                var textarea = option.FindElements(TextArea).FirstOrDefault(e => e.Displayed && e.Enabled);
                if (textarea != null)
                {
                    textarea.Clear();
                    textarea.SendKeys(FakerHelper.GetDescription());
                    continue;
                }

                // date field (faker)
                var date = option.FindElements(DateField).FirstOrDefault(e => e.Displayed && e.Enabled);
                if (date != null)
                {
                    date.Clear();
                    date.SendKeys(FakerHelper.GetFutureDate());
                }
            }
        }

        public List<Product> SelectRandomProductsAndAddToCart(int count)
        {
            actions.IsDisplayed(ProductNameLinks);
            var products = driver.FindElements(ProductNameLinks);

            Random random = new Random();
            List<Product> selectedProducts = new();

            for (int i = 0; i < count; i++)
            {
                products = driver.FindElements(ProductNameLinks);

                int randomIndex = random.Next(products.Count);
                var selectedProduct = products[randomIndex];
                string productName = selectedProduct.Text;

                selectedProduct.Click();
                actions.IsDisplayed(ProductTitle);

                var productDetails = GetProductDetails();

                FillMandatoryOptions();
                AddProductToCart(productDetails.ProductName);

                selectedProducts.Add(productDetails);

                driver.Navigate().Back();
                actions.IsDisplayed(ProductNameLinks);
            }

            return selectedProducts;
        }

        public List<ProductModel> AddProductsByName(List<string> productNames)
        {
            List<ProductModel> addedProducts = new();

            foreach (var name in productNames)
            {
                try
                {
                    driver.FindElement(By.LinkText(name)).Click();

                    actions.IsDisplayed(ProductTitle);

                    FillMandatoryOptions();

                    driver.FindElement(AddToCartButton).Click();

                    var productDetails = GetProductDetails();

                    addedProducts.Add(productDetails);

                    driver.Navigate().Back();
                    actions.IsDisplayed(ProductNameLinks);
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"Product not found: {name}");
                }
            }

            return addedProducts;
        }

        public Product GetProductDetails()
        {
            string name = actions.GetText(ProductTitle);
            string priceText = actions.GetText(ProductPrice);
            string exTaxText = actions.GetText(ExTaxPrice);
            string description = actions.GetText(ProductDescription);
            string image = actions.GetAttribute(ProductImage, "src").Trim();
            string brand = actions.GetText(ProductBrand);
            string code = actions.GetText(ProductCode);

            string quantityText = "1";
            string availability = "In Stock";

            return new Product
            {
                ProductName = name,
                ProductDescription = description,
                ProductBrand = brand,
                ProductCode = code,
                ProductAvailability = availability,
                ProductImage = image,
                Price = new Price
                {
                    IncludingTax = ParserHelper.ParsePrice(priceText),
                    ExcludingTax = ParserHelper.ParseExTaxPrice(exTaxText)
                },

                Quantity = ParserHelper.ParseQuantity(quantityText),
                UnitPrice = ParserHelper.ParsePrice(priceText),
                TotalPrice = ParserHelper.ParsePrice(priceText)
            };
        }

    }
}
