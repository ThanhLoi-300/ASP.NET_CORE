using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.SERVICE.Implementation;
using ASP.NET_CORE.SERVICE.Interface;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ServiceStack;
using Microsoft.AspNetCore.Authentication.Cookies;
using ASP.NET_CORE;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE.DATA.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc().AddRazorRuntimeCompilation();
// session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<Web_Core_DbContext>()
        .AddDefaultTokenProviders()
        .AddRoles<IdentityRole>()
        .AddRoleManager<RoleManager<IdentityRole>>()
        .AddSignInManager<SignInManager<ApplicationUser>>();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login_Page";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

//Khai bao Interface
builder.Services.AddScoped<ICategory, Category_Service>();
builder.Services.AddScoped<IProduct, Product_Service>();
builder.Services.AddScoped<ILogin, Login_Service>();
builder.Services.AddScoped<IOrder, Order_Service>();

IMvcBuilder IMB = builder.Services.AddRazorPages();
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

#if DEBUG
if (environment == Environments.Development)
{
    IMB.AddRazorRuntimeCompilation();
}
#endif

builder.Services.AddDbContext<Web_Core_DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection_String"));
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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Public")),
    RequestPath = "/Public"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Content")),
    RequestPath = "/Content"
});
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
// Thêm middleware session
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "login",
        pattern: "User/Account/Login",
        defaults: new { area = "User", controller = "Account", action = "Login_Page" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area}/{controller}/{action}/{id:int?}",
        defaults: new { area = "User", controller = "Account", action = "Login_Page" },
        constraints: new { id = @"\d+" },
        new[] { "ASP.NET_CORE.Areas.User.Controllers" });
});
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllers();
app.MapRazorPages();
app.Run();