using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var connString = builder.Configuration.GetConnectionString("DefaultConnectionMySQL");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connString, ServerVersion.AutoDetect(connString));
});

builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
