using LagoaTrading.Domain.Configurations;
using LagoaTrading.IoC.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LagoaTrading.IoC
{
    public static class LagoaTradingExtensions
    {
        public static void AddLagoaTrading(this IServiceCollection services, IConfiguration configuration)
        {

            var configurationNodeName = nameof(LagoaTradingConfiguration);
            var lagoaTradingConfiguration = configuration.GetSection(configurationNodeName).Get<LagoaTradingConfiguration>();
            if (lagoaTradingConfiguration == null)
            {
                throw new System.Exception($"Configuration node {configurationNodeName} not found");
            }


            services.AddAditional(lagoaTradingConfiguration);
            services.AddRepositories(lagoaTradingConfiguration);
            services.AddApplicationServices(lagoaTradingConfiguration);
        }
    }
}