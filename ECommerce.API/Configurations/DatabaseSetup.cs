namespace ECommerce.API.Configurations;

public static class DatabaseSetup
{
    public static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var Services = scope.ServiceProvider;
        var context = Services.GetRequiredService<ApplicationDBContext>();

        // Logger 
        var logger = Services.GetRequiredService<ILogger<Program>>();
        var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
        var userManager = Services.GetRequiredService<UserManager<AppUser>>();

        try
        {
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context);
            await UserIdentitySeed.SeedUserAsync(userManager);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error Occurred While Migrating Process");
        }
    }
}
