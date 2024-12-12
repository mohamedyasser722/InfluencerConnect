using Hangfire;
using InfluencerConnect.Infrastructure.Hangfire;

namespace InfluencerConnect.Api.Extensions;

public static class Hangfire
{
    public static void AddHangfire(this IServiceCollection services, WebApplication app)
    {
        services.AddScoped<RefreshTokenCleanupService>();

        // Configure Hangfire Dashboard with custom basic authentication
        app.UseHangfireDashboard("/jobs", new DashboardOptions
        {
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter(
                    app.Configuration.GetValue<string>("HangfireSettings:Username")!,
                    app.Configuration.GetValue<string>("HangfireSettings:Password")!)
            },
            DashboardTitle = "InfluencerConnect Dashboard",
            //IsReadOnlyFunc = context => true          // Uncomment this line to make the hangfire dashboard read-only
        });

        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var refreshTokenService = scope.ServiceProvider.GetRequiredService<RefreshTokenCleanupService>();

        RecurringJob.AddOrUpdate("RefreshTokenCleanup", () => refreshTokenService.CleanupExpiredTokensAsync(), Cron.Monthly);
    }
}
