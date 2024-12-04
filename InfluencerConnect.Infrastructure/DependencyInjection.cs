
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

        return services;
    }
}
