using LagoaTrading.Application.EmailService.Implementations;
using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Data.Service.HttpClients;
using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Interfaces.EmailServices;
using LagoaTrading.Domain.Interfaces.FoxbitServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LagoaTrading.IoC.Extensions
{
    internal static class AditionalExtensions
    {
        public static IServiceCollection AddAditional(this IServiceCollection services, LagoaTradingConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddDbContext<LagoaTradingContext>(options =>
            {
                var serverVersion = ServerVersion.AutoDetect(configuration.Security.ConnectionString);
                options.UseMySql(configuration.Security.ConnectionString, serverVersion);
            });

            services.AddSingleton<IFoxbitHttpClient, FoxbitHttpClient>();
            services.AddSingleton<IEmailService, EmailService>();
            return services;
        }
    }
}
