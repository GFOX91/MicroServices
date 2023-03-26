
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

namespace PlatformService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IPlatormRepo, PlatformRepo>();

            builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

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

            app.PrepPopulation();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}