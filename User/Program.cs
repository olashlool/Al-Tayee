using Admin.Data;
using Admin.Models.Identity;
using Admin.Models.Interface;
using Admin.Models.Services;
using Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddDbContext<AltayeeDBContext>(options => {
    // Our DATABASE_URL from js days
    string connectionString = Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddRazorPages()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc();

builder.Services.AddRazorPages()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AltayeeDBContext>();

builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    var supportedCultuers = new[]
    {
        new CultureInfo ("en"),
        new CultureInfo ("ar")
    };
    option.DefaultRequestCulture = new RequestCulture("en");
    option.SupportedUICultures = supportedCultuers;
});

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IBrands, BrandsServices>();
builder.Services.AddTransient<IProducts, ProductRepo>();
builder.Services.AddTransient<ITypes, TypesRepo>();
builder.Services.AddTransient<IImages, ImagesRepo>();
builder.Services.AddTransient<IAddresses, AddressesServices>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<IWishlistRepository, WishlistRepository>();
builder.Services.AddTransient<IContactUs, ContactUsServices>();
builder.Services.AddTransient<IOrder, OrderServices>();
builder.Services.AddScoped<ShoppingCart>();


var app = builder.Build();

var supportedCultures = new[] { "en", "ar" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization();
app.UseRequestLocalization(localizationOptions);

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
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();

