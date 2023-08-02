using LagoaTrading.Data.Repository.Repositories;
using LagoaTrading.Data.Service.Implementations;
using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Interfaces.FoxbitServices;
using LagoaTrading.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LagoaTrading.IoC.Extensions
{
    internal static class ApplicationRepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, LagoaTradingConfiguration configuration)
        {
            services.AddScoped<IApplicationRepositories, ApplicationRepositories>();
            services.AddScoped<IAnalysisRepository, AnalysisRepository>();
            services.AddScoped<ICandlestickRepository, CandlestickRepository>();
            services.AddScoped<ICircuitRepository, CircuitRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IInviteRepository, InviteRepository>();
            services.AddScoped<IMarketRepository, MarketRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<ISimulationRepository, SimulationRepository>();
            services.AddScoped<ISyncControlRepository, SyncControlRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IFoxbitRepository, FoxbitRepository>();

            return services;
        }
    }
}
