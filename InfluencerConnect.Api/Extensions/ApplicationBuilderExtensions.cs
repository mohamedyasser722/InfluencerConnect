using InfluencerConnect.Api.Middleware;
using InfluencerConnect.Infrastructure;

namespace InfluencerConnect.Api.Extensions;

public static class ApplicationBuilderExtensions
{

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
    public static void UseTokenValidationMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<TokenValidationMiddleware>();
    }
}
