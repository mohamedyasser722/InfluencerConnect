using InfluencerConnect.Application.Abstractions.Email;
using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Infrastructure.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IUnitOfWork>(services => services.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
