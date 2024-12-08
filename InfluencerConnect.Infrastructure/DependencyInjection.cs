
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InfluencerConnect.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InfluencerConnect.Application.Abstractions.Email;
using InfluencerConnect.Infrastructure.Email;
using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.Influencers;
using InfluencerConnect.Infrastructure.Repositories;
using System.Runtime.CompilerServices;
using InfluencerConnect.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using InfluencerConnect.Infrastructure.Authentication;
using InfluencerConnect.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;

namespace InfluencerConnect.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IUnitOfWork>(services => services.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IInfluencerRepository, InfluencerRepository>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection  services)
    {
        // Register IHttpContextAccessor for accessing HTTP context
        services.AddHttpContextAccessor();

        // Register ICurrentUserService with CurrentUserService implementation
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddIdentity(); 
        services.AddJwtBearer();
        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
    private static IServiceCollection AddJwtBearer(this IServiceCollection services)
    {

        services.AddAuthentication(services =>
        {
            services.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            services.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(services =>
            {
                services.SaveToken = true;
                services.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("D34D070C939D4A8D8F3AD1584EF3FF61")),
                    ValidateIssuer = true,
                    ValidIssuer = "InfluencerConnect",
                    ValidateAudience = true,
                    ValidAudience = "InfluencerConnect Users",
                    ValidateLifetime = true,
                };
            });

        return services;
    }

}
