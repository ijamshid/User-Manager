using UserManager.Application;
using UserManager.Infrastructure;
using UserManager.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Kesh va sessiya
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    // Zarurat bo‘lsa, boshqa sozlamalar
});

// DI servislar
builder.Services
    .AddDatabase(configuration)
    .AddApplication()
    .AddJwtAuthentication(configuration)
    .AddConfigurations(configuration)
    .AddSessions();

// Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// 🔁 Avtomatik migratsiya
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await AutomatedMigration.MigrateAsync(scope.ServiceProvider);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Migration failed.");
    }
}

// ✅ Production uchun xavfsizlik middleware’lari
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // HSTS - HTTPS majburiyligi
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
