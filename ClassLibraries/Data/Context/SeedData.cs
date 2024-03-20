using Data.Constants;
using Data.Entity;
using Data.Entity.EntityMVC;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Context
{
    public class SeedData
    {
        private readonly AppDbContext _context;

        public SeedData(AppDbContext context)
        {
            _context = context;
        }

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var roleName in new[] { RoleConstant.BuyerRole, RoleConstant.AdminRole, RoleConstant.SellerRole })
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = roleName });
                }
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var user = await userManager.FindByEmailAsync("user1@example.com");
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, RoleConstant.BuyerRole);
            }
        }

        public void Initialize()
        {
            if (_context.Users.Any())
            {
                return;
            }

            var users = new List<User>
            {
                new User
                {
                    Email = "user1@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    Password = "password1",
                    RoleId = 3,
                    Enabled = true,
                    CreatedAt = DateTime.Now
                },
            };

            var roles = new List<Role>
            {
                new Role
                {
                    Name = "Admin",
                    CreatedAt = DateTime.Now
                },
            };

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Electronics",
                    Color = "Blue",
                    IconCssClass = "fas fa-mobile-alt",
                    CreatedAt = DateTime.Now
                },
            };

            var products = new List<Product>
            {
                new Product
                {
                    SellerId = 1,
                    CategoryId = 1,
                    Name = "Smartphone",
                    Price = 999.99m,
                    Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    StockAmount = 100,
                    CreatedAt = DateTime.Now,
                    Enabled = true
                },
            };

            var productImages = new List<ProductImage>
            {
                new ProductImage
                {
                    ProductId = 1,
                    Url = "https://example.com/image1.jpg",
                    CreatedAt = DateTime.Now
                },
            };

            var productComments = new List<ProductComment>
            {
                new ProductComment
                {
                    ProductId = 1,
                    UserId = 1,
                    Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    StarCount = 5,
                    IsConfirmed = true,
                    CreatedAt = DateTime.Now
                },
            };

            var orders = new List<Order>
            {
                new Order
                {
                    UserId = 1,
                    OrderCode = "ABC123",
                    Address = "123 Main St, Anytown, USA",
                    CreatedAt = DateTime.Now
                },
            };

            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 1,
                    UnitPrice = 999.99m,
                    CreatedAt = DateTime.Now
                },
            };


            var cartItems = new List<CartItem>
            {
                new CartItem
                {
                    UserId = 1,
                    ProductId = 1,
                    Quantity = 1,
                    CreatedAt = DateTime.Now
                },
            };

            var contactMessages = new List<ContactMessage>
            {
                new ContactMessage
                {
                    UserId = 11,
                    UserName = "John Doe",
                    UserEmail = "user1@example.com",
                    ProductId = 1,
                    ProductName = "Smartphone",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    CreatedAt = DateTime.Now
                },
            };

            _context.Users.AddRange(users);
            _context.Roles.AddRange(roles);
            _context.Categories.AddRange(categories);
            _context.Products.AddRange(products);
            _context.ProductImages.AddRange(productImages);
            _context.ProductComments.AddRange(productComments);
            _context.Orders.AddRange(orders);
            _context.OrderItems.AddRange(orderItems);
            _context.CartItems.AddRange(cartItems);
            _context.contactMessages.AddRange(contactMessages);

            _context.SaveChanges();
        }
    }
}


/*public void Initialize()
{
    if (_context.Users.Any())
    {
        return;
    }

    var userIds = 1;
    var roleIds = 1;
    var categoryIds = 1;
    var productIds = 1;
    var orderIds = 1;

    var userFaker = new Faker<User>()
        .RuleFor(u => u.Id, f => userIds++)
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.FirstName, f => f.Name.FirstName())
        .RuleFor(u => u.LastName, f => f.Name.LastName())
        .RuleFor(u => u.Password, f => f.Internet.Password())
        .RuleFor(u => u.RoleId, f => f.Random.Number(1, 3))
        .RuleFor(u => u.Enabled, f => f.Random.Bool())
        .RuleFor(u => u.CreatedAt, f => f.Date.Recent());

    var roleFaker = new Faker<Role>()
        .RuleFor(r => r.Id, f => roleIds++)
        .RuleFor(r => r.Name, f => f.Random.ArrayElement(new string[] { "Admin", "Seller", "Buyer" }))
        .RuleFor(r => r.CreatedAt, f => f.Date.Recent());

    var categoryFaker = new Faker<Category>()
        .RuleFor(c => c.Id, f => categoryIds++)
        .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
        .RuleFor(c => c.Color, f => f.Commerce.Color())
        .RuleFor(c => c.IconCssClass, f => f.Random.ArrayElement(new string[] { "fas fa-mobile-alt", "fas fa-laptop", "fas fa-tablet-alt" }))
        .RuleFor(c => c.CreatedAt, f => f.Date.Recent());

    var productFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => productIds++)
        .RuleFor(p => p.SellerId, f => f.Random.Number(1, 10))
        .RuleFor(p => p.CategoryId, f => f.Random.Number(1, 10))
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Finance.Amount())
        .RuleFor(p => p.Details, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.StockAmount, f => f.Random.Number(1, 100))
        .RuleFor(p => p.CreatedAt, f => f.Date.Recent())
        .RuleFor(p => p.Enabled, f => f.Random.Bool());

    var orderFaker = new Faker<Order>()
        .RuleFor(o => o.Id, f => orderIds++)
        .RuleFor(o => o.UserId, f => f.Random.Number(1, 10))
        .RuleFor(o => o.OrderCode, f => f.Random.AlphaNumeric(6))
        .RuleFor(o => o.Address, f => f.Address.FullAddress())
        .RuleFor(o => o.CreatedAt, f => f.Date.Recent());

    var users = userFaker.Generate(10);
    var roles = roleFaker.Generate(3); // Assuming 3 roles: Admin, Seller, Buyer
    var categories = categoryFaker.Generate(10);
    var products = productFaker.Generate(10);
    var orders = orderFaker.Generate(10);

    // For the remaining entities (ProductImage, ProductComment, OrderItem, CartItem, ContactMessage),
    // you can create similar Faker instances and generate data as shown above.

    _context.Users.AddRange(users);
    _context.Roles.AddRange(roles);
    _context.Categories.AddRange(categories);
    _context.Products.AddRange(products);
    // Add the remaining entities to the context and save changes as shown above.
    _context.SaveChanges();
}
*/
