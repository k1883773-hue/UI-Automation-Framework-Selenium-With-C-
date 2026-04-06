using Bogus;

namespace NaveenAutomationPOM.Utilities
{
    public static class FakerHelper
    {
        private static readonly Faker faker = new("en");

        public static string GetName()
        {
            return faker.Name.FirstName();
        }

        public static string GetDescription()
        {
            return faker.Lorem.Sentence();
        }

        public static string GetFutureDate()
        {
            return faker.Date.Future().ToString("yyyy-MM-dd");
        }
    }
}
