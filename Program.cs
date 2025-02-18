using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // ✅ Ensures Identity UI is enabled

// Configure SQLite Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlite(connectionString));

// Add Identity and configure it
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Stores.MaxLengthForKeys = 128;
})
.AddEntityFrameworkStores<BlogContext>()
.AddRoles<IdentityRole>()
.AddDefaultUI()
.AddDefaultTokenProviders();

var app = builder.Build();

// Middleware Pipeline
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ✅ Ensure Razor Pages and Controllers are mapped
app.MapRazorPages(); // ✅ Enables Identity UI
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// var hasher = new PasswordHasher<object>();

//         string password = "P@$$w0rd"; // Change if needed
//         string hash = hasher.HashPassword(null, password);

//         Console.WriteLine($"New Hashed Password: {hash}"); //AQAAAAIAAYagAAAAEL1kFMkfUW5F66UnH+Ho0BIqUuCd7bgja2NmOMRjM6igGvbCOVZ9X4RVIdtPiefuug==

app.Run();



