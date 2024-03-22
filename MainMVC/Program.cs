using Data.Context;
using Microsoft.EntityFrameworkCore;
using MainMVC.Services.HomeServices;
using MainMVC.Services.ProductServices;
using Data.Entity;
using Microsoft.AspNetCore.Identity;
using MainMVC.Services.AuthServices;
using MainMVC.Services.CartServices;
using MainMVC.Services.OrderServices;
using Data.Services;
using Data.Constants;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(RoleConstant.AdminRole));
    options.AddPolicy("RequireSellerRole", policy => policy.RequireRole(RoleConstant.SellerRole));
    options.AddPolicy("RequireBuyerRole", policy => policy.RequireRole(RoleConstant.BuyerRole));
});

//Services 
//AuthServices
builder.Services.AddScoped<DataRepository<User>>();
builder.Services.AddScoped<AuthService>();

//CartServices
builder.Services.AddScoped<CartService>();

//HomeServices
builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<ContactService>();

//OrderServices
builder.Services.AddScoped<OrderService>();

//ProductService
builder.Services.AddScoped<ProductService>();

//ProfileServices
builder.Services.AddScoped<ProfileService>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=OnlineTrading;User Id=sa;Password=reallyStrongPwd123;Encrypt=true;TrustServerCertificate=True;"));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Identity ayarlar�
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie ayarlar�
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/Login";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});



var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

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
    pattern: "{controller=Auth}/{action=Register}/{id?}");

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); 
}

// Seed verilerini ekle
using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<AppDbContext>();

    // E�er veritaban�nda kullan�c� varsa seed i�lemini yapma
    if (!context.Users.Any())
    {
        var seedData = new SeedData(context);
        seedData.Initialize();
        Console.WriteLine("Veritaban� ba�ar�yla seedlendi.");
    }
    else
    {
        Console.WriteLine("Veritaban�nda zaten kullan�c�lar mevcut, seed i�lemi yap�lmad�.");
    }
}
app.Run();
