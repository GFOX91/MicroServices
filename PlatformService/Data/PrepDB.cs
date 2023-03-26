using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

/// <summary>
/// Used to setup, seed and test the Database
/// </summary>
public static class PrepDB
{
    /// <summary>
    /// Retrieves the service scopes calls method to seed data utilizing DB context
    /// </summary>
    public static void PrepPopulation(this WebApplication app, bool isProduction)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
        }
    }

    /// <summary>
    /// Will Apply data migrations if we are in production enviroment and
    /// seed the DB with data if none already exists
    /// </summary>
    /// <param name="context"></param>
    private static void SeedData(AppDbContext context, bool isProduction)
    {
        if(isProduction)
        {
            Console.WriteLine("--> Attempting to Apply Migrations...");

            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> could not run migrations: {ex.Message}");
                throw;
            }

            context.Database.Migrate();
        }

        if (!context.Platforms.Any()) 
        {
            Console.WriteLine("--> Seeding Data...");

            context.Platforms.AddRange(
                new Platform() { Name = "Dot Net", Publisher ="Microsoft", Cost="Free"},
                new Platform() { Name = "SQL Server Express", Publisher ="Microsoft", Cost="Free"},
                new Platform() { Name = "Kubernetes", Publisher ="Cloud NAtive Computing Foundation", Cost="Free"}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
}
