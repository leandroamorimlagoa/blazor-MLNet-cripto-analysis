using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly LagoaTradingContext context;

        public CurrencyRepository(LagoaTradingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Currency>> GetAll()
        => await this.context.Currency.ToListAsync();

        public async Task Save(Currency currency)
        {
            if (currency.Id == 0)
            {
                await this.context.Currency.AddAsync(currency);
            }
            else
            {
                this.context.Currency.Update(currency);
            }
            await this.context.SaveChangesAsync();
        }
    }
}
