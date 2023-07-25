using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using MediatR;
using WEB.Models;
using MediatR.Registration;
using LazyCache;    
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;
string connectionString = configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDBContext>(c => c.UseSqlServer(connectionString));

//-----------------------------------------//
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddLazyCache();

builder.Services.Configure<RazorViewEngineOptions>(o =>
{
    o.ViewLocationFormats.Clear();
    o.ViewLocationFormats.Add("/Pages/Home/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Cars/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Account/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/CarType/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/DriveType/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Fuel/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Transmission/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/OrderPage/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/CarBody/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Reservation/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Shared/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/{0}" + RazorViewEngine.ViewExtension);
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();