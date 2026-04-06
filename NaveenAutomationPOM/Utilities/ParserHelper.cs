namespace NaveenAutomationPOM.Utilities
{
    public static class ParserHelper
    {
        public static decimal ParsePrice(string text)
        {
            return decimal.Parse(text.Replace("$", "").Trim());
        }

        public static decimal ParseExTaxPrice(string text)
        {
            return decimal.Parse(
                text.Replace("Ex Tax:", "")
                    .Replace("$", "")
                    .Trim()
            );
        }
        public static int ParseQuantity(string text)
        {
            return int.Parse(
                text.Replace("Qty:", "")
                    .Trim()
            );
        }
    }
}