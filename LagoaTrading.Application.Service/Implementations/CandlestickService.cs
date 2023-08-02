using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.Repositories;

namespace LagoaTrading.Application.Service.Implementations
{
    public class CandlestickService : ICandlestickService
    {
        private readonly IApplicationRepositories applicationRepositories;

        public CandlestickService(IApplicationRepositories applicationRepositories)
        {
            this.applicationRepositories = applicationRepositories;
        }

        public async Task GenerateFile()
        {
            var candlesticks = await this.applicationRepositories.CandlestickRepository.GetAll();
            using (StreamWriter sw = new StreamWriter("candlesticks.csv"))
            {
                sw.WriteLine(string.Join(",", candlesticks.First().GetType().GetProperties().Select(p => p.Name)));

                foreach (var row in candlesticks)
                {
                    sw.WriteLine(string.Join(",", row.GetType().GetProperties().Select(p => p.GetValue(row, null))));
                }
            }
        }

        public Task Save(IEnumerable<Candlestick> candlesticks)
        => this.applicationRepositories.CandlestickRepository.Save(candlesticks);
    }
}
