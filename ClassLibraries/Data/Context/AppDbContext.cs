using Data.Entity;
using Data.Entity.EntityMVC;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        // MainMvc Entity
        public DbSet<ContactMessage> contactMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(c => c.Color)
                .HasMaxLength(50);

            // CartItem - User ilişkisi
            modelBuilder.Entity<CartItem>()
                        .HasOne(p => p.User)
                        .WithMany()
                        .HasForeignKey(p => p.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

            // CartItem - Product ilişkisi
            modelBuilder.Entity<CartItem>()
                        .HasOne(p => p.Product)
                        .WithMany()
                        .HasForeignKey(p => p.ProductId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Product - Seller ilişkisi
            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Seller)
                        .WithMany()
                        .HasForeignKey(p => p.SellerId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Product - Category ilişkisi
            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Category)
                        .WithMany()
                        .HasForeignKey(p => p.CategoryId)
                        .OnDelete(DeleteBehavior.Restrict);

            // ProductComment - Product ilişkisi
            modelBuilder.Entity<ProductComment>()
                        .HasOne(p => p.Product)
                        .WithMany(p => p.ProductComments)
                        .HasForeignKey(p => p.ProductId)
                        .OnDelete(DeleteBehavior.Restrict);

            // ProductComment - User ilişkisi
            modelBuilder.Entity<ProductComment>()
                        .HasOne(p => p.User)
                        .WithMany()
                        .HasForeignKey(p => p.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

            // OrderItem - Order ilişkisi
            modelBuilder.Entity<OrderItem>()
                        .HasOne(p => p.Order)
                        .WithMany(p => p.OrderItems)
                        .HasForeignKey(p => p.OrderId)
                        .OnDelete(DeleteBehavior.Restrict);

            // OrderItem - Product ilişkisi
            modelBuilder.Entity<OrderItem>()
                        .HasOne(p => p.Product)
                        .WithMany()
                        .HasForeignKey(p => p.ProductId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Order - User ilişkisi
            modelBuilder.Entity<Order>()
                        .HasOne(p => p.User)
                        .WithMany(p => p.Order)
                        .HasForeignKey(p => p.UserId)
                        .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);

        }
    }
}
