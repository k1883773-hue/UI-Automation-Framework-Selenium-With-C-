using Bogus;
using NaveenAutomationPOM.DataObjects.Register;

namespace NaveenAutomationPOM.DataFactory.RegisterData
{
    public static class RegisterData
    {
        private static readonly Faker faker = new("en");
        public static Register GenerateValidRegisterData()
        {
            var password = faker.Internet.Password(8, true);

            return new Register
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Email = faker.Internet.Email(),
                Telephone = faker.Phone.PhoneNumber("9#########"),
                Password = password,
                SubscribeNewsletter = faker.Random.Bool(),
                AcceptPrivacyPolicy = true
            };
        }

        public static Register GenerateUserWithExistingEmail()
        {
            var user = GenerateValidRegisterData();
            user.Email = "test@yopmail.com";
            return user;
        }

        public static (Register user, string confirmPassword) GenerateUserWithMismatchedPassword()
        {
            var user = GenerateValidRegisterData(); 
            return (user, "DifferentPassword123"); // mismatch only
        }
    }
}