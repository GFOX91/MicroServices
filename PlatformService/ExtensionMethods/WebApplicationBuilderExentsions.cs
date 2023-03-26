using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

namespace PlatformService.ExtensionMethods;

public static class WebApplicationBuilderExentsions
{
    /// <summary>
    /// Configures the Database, will utilize an InMemory Database in the development enviroment
    /// or SQL Server in Production
    /// </summary>
    /// <param name="builder">A Microsoft.AspNetCore.Builder.WebApplicationBuilder</param>
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var app = builder.Build();

        if (app.Environment.IsProduction())
        {
            Console.WriteLine("--> using SQL Server DB");
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:PlatformsConn")));
        }
        else
        {
            Console.WriteLine("--> using InMem DB");
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("InMemoryDB"));
        }
    }
}
