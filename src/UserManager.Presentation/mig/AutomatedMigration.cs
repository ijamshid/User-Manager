using Microsoft.EntityFrameworkCore;
using UserManager.Infrastructure;

public static class AutomatedMigration
{
    public static async Task MigrateAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        if (context.Database.IsNpgsql())
            await context.Database.MigrateAsync();
    }
}
