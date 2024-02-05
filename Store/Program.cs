using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Store.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/User/Index");

string connectionProducts = builder.Configuration["Data:MyStoreProducts:Connectionstring"]!;
string connectionUsers = builder.Configuration["Data:MyStoreUsers:Connectionstring"]!;

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionProducts));
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionUsers));

builder.Services.AddScoped<IProductRepository, EFProductRepository>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();



var app = builder.Build();


app.UseStaticFiles();
app.UseSession();
app.UseHttpsRedirection();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: null,
    pattern: "{category}/Page{productPage:int}",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: null,
    pattern: "Page{productPage:int}",
    defaults: new
    {
        controller = "Home",
        action = "Index",
        productPage = 1
    });

app.MapControllerRoute(
    name: null,
    pattern: "{category}",
    defaults: new
    {
        controller = "Home",
        action = "Index",
        productPage = 1
    }
    );

app.MapControllerRoute(
    name: null,
    pattern: "",
    defaults: new
    {
        controller = "Home",
        action = "Index",
        productPage = 1
    });

app.MapControllerRoute(name: null, pattern: "{controller}/{action}/{id?}");


app.Run();
