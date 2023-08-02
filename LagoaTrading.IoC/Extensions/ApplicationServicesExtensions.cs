using LagoaTrading.Application.Service;
using LagoaTrading.Application.Service.Implementations;
using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace LagoaTrading.IoC.Extensions
{
    internal static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, LagoaTradingConfiguration configuration)
        {
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IAnalysisService, AnalysisService>();
            services.AddScoped<ICandlestickService, CandlestickService>();
            services.AddScoped<ICircuitService, CircuitService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IFoxbitService, FoxbitService>();
            services.AddScoped<IInviteService, InviteService>();
            services.AddScoped<IMarketService, MarketService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ISimulationService, SimulationService>();
            services.AddScoped<ISyncControlService, SyncControlService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISecurityService, SecurityService>();

            return services;
        }
    }
}
