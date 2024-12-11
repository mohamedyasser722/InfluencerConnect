
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
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.RegisterAppServicesDI(configuration);

        services.AddAuth(configuration);


        return services;
    }
    private static IServiceCollection RegisterAppServicesDI(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddTransient<IEmailService, EmailService>();

        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddScoped<IUnitOfWork>(services => services.GetRequiredService<ApplicationDbContext>());


        services.AddScoped<IInfluencerRepository, InfluencerRepository>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection  services, IConfiguration configuration)
    {

        // Register IHttpContextAccessor for accessing HTTP context
        services.AddHttpContextAccessor();


        services.AddIdentityAuth(); 
        services.AddJwtBearerAuth(configuration);
        return services;
    }

    private static IServiceCollection AddIdentityAuth(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
    private static IServiceCollection AddJwtBearerAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var _jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions!.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtOptions!.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtOptions.Audience,
                    ValidateLifetime = true,
                };
            });

        return services;
    }

}
