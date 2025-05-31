using AutoFix.Data;
using AutoFix.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// Register MongoDB context with error handling
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<MongoDbContext>>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>();
    
    try
    {
        var context = new MongoDbContext(settings, logger);
        logger.LogInformation("MongoDB context initialized");
        return context;
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to initialize MongoDB: {Message}", ex.Message);
        // Still return the context, but log the error
        // The application will handle MongoDB errors gracefully at runtime
        return new MongoDbContext(settings, logger);
    }
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddLogging(logging => 
{
    logging.ClearProviders();
    logging.AddConsole(options => 
    {
        options.FormatterName = "Simple";
    });
    logging.AddDebug();
});

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
// Add Razor Pages support
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions
    {
        SourceCodeLineCount = 10
    });
}
else
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

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
