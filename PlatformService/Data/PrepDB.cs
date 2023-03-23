namespace PlatformService.Data;

/// <summary>
/// Used to setup, seed and test the Database
/// </summary>
public static class PrepDB
{
    public static void PrepPopulation(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {

        }
    }

    private static void SeedData(AppDbContext context)
    {

    }
}
