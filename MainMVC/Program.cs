using Data.Context;
using Microsoft.EntityFrameworkCore;
using MainMVC.Services.HomeServices;
using MainMVC.Services.ProductServices;
using Data.Entity;
using Microsoft.AspNetCore.Identity;
using MainMVC.Services.AuthServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//HomeServices
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<ListingService>();
builder.Services.AddScoped<ProductService>();

//ProductService
builder.Services.AddScoped<CreateService>();
builder.Services.AddScoped<DeleteService>();
builder.Services.AddScoped<EditService>();
builder.Services.AddScoped<ProductDetailService>();


//AuthServices
builder.Services.AddScoped<AuthService>();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=OnlineTrading;User Id=sa;Password=reallyStrongPwd123;Encrypt=true;TrustServerCertificate=True;"));

// ConfigureServices metoduna aþaðýdaki kodlarý ekleyin
// AddIdentity ve AddEntityFrameworkStores metodlarýný düzeltin
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Identity ayarlarý
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>() // int tipini kullanýcý kimliði olarak belirtin
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie ayarlarý
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.SlidingExpiration = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // veya EnsureCreatedAsync() kullanýlabilir
}

// Seed verilerini ekle
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        SeedData.Initialize(context);

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
app.Run();
