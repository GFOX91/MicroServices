
using CommandsService.AsyncDataServices;
using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.ExtensionMethods;
using PlatformService.SyncDataServices.Http;

namespace PlatformService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.ConfigureDatabase();

            builder.Services.AddControllers();
            builder.Services.AddHostedService<MessageBusSubscriber>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IPlatormRepo, PlatformRepo>();
            builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

            builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

            var app = builder.Build();

            app.PrepPopulation(app.Environment.IsProduction());

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