using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Shared.Core;

namespace LagoaTrading.Server.IntegrationManagers
{
    public class SyncCurrency : BaseSync
    {
        public SyncCurrency(IApplicationService applicationService)
                     : base(Constants.SyncControlNames.CurrenciesName, applicationService)
        {
        }

        internal async Task Execute()
        {
            var foxbitCurrencies = await this.applicationService.FoxbitService.GetCurrencies();
            if (foxbitCurrencies == null)
            {
                return;
            }
            var localCurrencies = await this.applicationService.CurrencyService.GetAll();
            if (localCurrencies == null)
            {
                return;
            }

            var foxbitCurrenciesList = foxbitCurrencies.Select(c => new Currency()
            {
                Symbol = c.Symbol,
                Name = c.Name,
                Type = c.Type,
                Precision = c.Precision
            })
                                                        .ToList();

            foreach (var item in foxbitCurrenciesList)
            {
                var localCurrency = localCurrencies.FirstOrDefault(c => c.Symbol == item.Symbol);
                if(localCurrency == null)
                {
                    localCurrency = new Currency();
                }
                localCurrency.Symbol = item.Symbol;
                localCurrency.Name = item.Name;
                localCurrency.Type = item.Type;
                localCurrency.Precision = item.Precision;

                await this.applicationService.CurrencyService.Save(localCurrency);
            }
            await RegisterSync(Constants.SyncControlNames.CurrenciesName);
        }
    }
}
