using Bogus;
using Data.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Data.Context
{
    public class SeedData
    {
        private static List<User> _users;
        private static List<Product> _products;

        public static void Initialize(AppDbContext context)
        {
            // Eğer veritabanında kullanıcı varsa seed işlemini yapma
            if (context.Users.Any())
            {
                return;
            }

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName());

            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.Details, f => f.Lorem.Sentence())
                .RuleFor(p => p.StockAmount, f => f.Random.Byte(0, 100))
                .RuleFor(p => p.CreatedAt, f => f.Date.Past())
                .RuleFor(p => p.Enabled, f => f.Random.Bool());

            var users = userFaker.Generate(10);
            var products = productFaker.Generate(10);

            // Kullanıcıları ve ürünleri ekleyin
            context.Users.AddRange(users);
            context.Products.AddRange(products);

            // Değişiklikleri kaydedin
            context.SaveChanges();
        }
    }
}
