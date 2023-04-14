using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.SERVICE.Implementation;
using ASP.NET_CORE.SERVICE.Interface;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ServiceStack;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Khai bao Interface
builder.Services.AddScoped<ICategory, Category_Service>();
builder.Services.AddScoped<IProduct, Product_Service>();

IMvcBuilder IMB = builder.Services.AddRazorPages();
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

#if DEBUG
    if(environment == Environments.Development)
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

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Areas",
      pattern: "{area:exists}/{controller=Category}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Layout_Index}/{action=Index}/{id?}"
    );
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Public")),
    RequestPath = "/Public"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Content")),
    RequestPath = "/Content"
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
