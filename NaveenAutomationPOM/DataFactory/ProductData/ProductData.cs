using Bogus;

namespace NaveenAutomationPOM.DataFactory.ProductData
{
    public static class ProductData
    {
        // Search Product - Invalid
        public static string GetInvalidProductName()
        {
            var faker = new Faker();
            return faker.Commerce.ProductName() + faker.Random.Number(1000, 9999);
        }

        // Multiple Search Products & Valid Search Product
        public static List<string> GetValidProductNames()
        {
            return
            [
                "MacBook",
                "iPhone",
                "iPod Touch",
                "HP LP3065",
                "iPod Nano"
            ];
        }
    }
}
