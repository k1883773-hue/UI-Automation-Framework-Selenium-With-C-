namespace NaveenAutomationPOM.DataObjects.Product

{
    public class Product
    {
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }
        public required Price Price { get; set; }
        public required string ProductImage { get; set; }
        public required string ProductBrand { get; set; }
        public required string ProductCode { get; set; }
        public required string ProductAvailability { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class Price
    {
        public decimal IncludingTax { get; set; }
        public decimal ExcludingTax { get; set; }
    }
}
