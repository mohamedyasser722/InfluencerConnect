

using Hangfire;
using InfluencerConnect.Api.Extensions;
using InfluencerConnect.Api.Middleware;
using InfluencerConnect.Application;
using InfluencerConnect.Infrastructure;
using InfluencerConnect.Infrastructure.Hangfire;
using System.Runtime.CompilerServices;
namespace InfluencerConnect.Api;

public class Program
{
   
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSwagger();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();


        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.MapOpenApi();
        }
        app.UseCustomExceptionHandler();

        app.UseHttpsRedirection();

        //builder.Services.AddHangfire(app);

        app.UseTokenValidationMiddleware();
        app.UseAuthorization();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
