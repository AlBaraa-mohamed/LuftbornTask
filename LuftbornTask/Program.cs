using LuftbornTask.src.Application.Services;
using LuftbornTask.src.Domain.Interfaces;
using LuftbornTask.src.Infrastructure.Data;
using LuftbornTask.src.Infrastructure.Repositories;
using LuftbornTask.src.WebUI.Decorators;
using LuftbornTask.src.WebUI.Strategies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       IConfigurationSection googleAuthNSection =
       builder.Configuration.GetSection("Authentication:Google");
       options.ClientId = googleAuthNSection["ClientId"];
       options.ClientSecret = googleAuthNSection["ClientSecret"];
   });

// Register repository and services with decorator pattern
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.Decorate<IProductRepository, LoggingProductRepositoryDecorator>();

// Register pricing strategies individually
builder.Services.AddScoped<RegularPriceStrategy>();
builder.Services.AddScoped<DiscountPriceStrategy>();
builder.Services.AddScoped<PremiumPriceStrategy>();

// Strategy factory to resolve user-selected strategy
builder.Services.AddScoped<StrategyFactory>(provider =>
{
    return strategyName => strategyName switch
    {
        "Regular" => provider.GetRequiredService<RegularPriceStrategy>(),
        "Discount" => provider.GetRequiredService<DiscountPriceStrategy>(),
        "Premium" => provider.GetRequiredService<PremiumPriceStrategy>(),
        _ => throw new ArgumentException("Invalid strategy", nameof(strategyName))
    };
});

builder.Services.AddScoped<ProductService>(provider =>
{
    var repo = provider.GetRequiredService<IProductRepository>();
    var factory = provider.GetRequiredService<StrategyFactory>();
    return new ProductService(repo, factory);
});


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// Optional custom error page fallback
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

// Global exception handling
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/plain";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exceptionHandlerPathFeature?.Error, "An unhandled exception occurred.");

        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    });
});
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
