using InfluencerConnect.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace InfluencerConnect.Api.Middleware;
public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TokenValidationMiddleware> _logger;

    public TokenValidationMiddleware(
        RequestDelegate next,
        ILogger<TokenValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ITokenBlacklistService tokenBlacklistService)
    {
        try
        {
            var token = GetTokenFromHeader(context);
            if (!string.IsNullOrEmpty(token))
            {
                if (await tokenBlacklistService.IsTokenBlacklistedAsync(token))
                {
                    _logger.LogWarning("Blacklisted token detected: {Token}", token);

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = "Unauthorized",
                        Detail = "The provided token has been blacklisted.",
                    });
                    return;
                }
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while validating the token.");
            throw; // Pass the exception to the ExceptionHandlingMiddleware.
        }
    }

    private static string? GetTokenFromHeader(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        return authorizationHeader?.StartsWith("Bearer ") == true
            ? authorizationHeader["Bearer ".Length..].Trim()
            : null;
    }
}
