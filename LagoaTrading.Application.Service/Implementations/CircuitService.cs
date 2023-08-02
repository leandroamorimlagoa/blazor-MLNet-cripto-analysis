using LagoaTrading.Application.Service.Extensions;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.FoxbitServices;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Domain.Objects.FoxbitObjects.Requests;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Application.Service.Implementations
{
    public class CircuitService : ICircuitService
    {
        protected readonly IApplicationRepositories repositories;
        private readonly IFoxbitRepository foxbitRepository;

        public CircuitService(IApplicationRepositories repositories,
                                IFoxbitRepository foxbitRepository)
        {
            this.repositories = repositories;
            this.foxbitRepository = foxbitRepository;
        }

        public async Task<IEnumerable<CircuitResponse>> GetCircuitList(User user, CircuitRequest query)
        => await this.repositories.CircuitRepository.GetAllFromUser(user, query);

        public Task<bool> EndCircuit(long circuitId)
        => this.repositories.CircuitRepository.EndCircuit(circuitId);

        public async Task<bool> StartCircuit(User user, Parameter parameter, CircuitType circuitType)
        {
            if (user == null
                || parameter == null
                || parameter.AvaliableValue < 1)
            {
                return false;
            }


            var analysisResult = await this.repositories.AnalysisRepository.GetAnalysis(user, user.Parameter, 24);
            if (analysisResult == null)
            {
                return false;
            }

            var market = await this.repositories.MarketRepository.Get(analysisResult.MarketId);
            if (market == null)
            {
                return false;
            }

            var simulationBuy = await this.repositories.SimulationRepository.SimulateBuy(user.Parameter, analysisResult.MarketId);
            if (simulationBuy == null)
            {
                return false;
            }

            var apiSecret = CryptoHelper.Decrypt(user.Parameter.ApiSecret, user.EmailHash);
            var apiKey = CryptoHelper.Decrypt(user.Parameter.ApiKey, user.EmailHash);

            var requestNewOrder = new RequestCreateOrder()
            {
                ApiSecret = apiSecret,
                ApiKey = apiKey,
                Side = Side.Buy.ToString().ToUpper(),
                MarketSymbol = market.Symbol,
                Quantity = simulationBuy.Quantity.ToString(),
                UnitPrice = simulationBuy.UnitPrice.ToString(),
            };

            var responseNewOrder = await this.foxbitRepository.CreateOrder(requestNewOrder);
            if (responseNewOrder == null)
            {
                return false;
            }
            var position = requestNewOrder.ToPosition(responseNewOrder, user.Id, analysisResult.MarketId);

            parameter.AvaliableValue -= (simulationBuy.Quantity * simulationBuy.UnitPrice).TruncateCurrency();
            parameter.CircuitCommand = circuitType == CircuitType.Continuous
                                            ? CircuitCommand.ExecutingAutomatic
                                            : CircuitCommand.ExecutingManual;
            await this.repositories.UserRepository.SaveParameter(parameter);

            await repositories.CircuitRepository.StartCircuit(position, circuitType);
            await repositories.UserRepository.UpdateCircuitCommand(parameter.Id, CircuitCommand.ExecutingManual);

            return true;
        }

        public Task<Circuit?> Get(long id)
        => this.repositories.CircuitRepository.Get(id);

        public async Task<bool> CreatePositionToSell(long value, Position position)
        {
            var user = await this.repositories.UserRepository.Get(position.UserId);
            var parameter = user?.Parameter;
            var market = await this.repositories.MarketRepository.Get(position.MarketId);

            if (user == null
                || parameter == null
                || market == null
                || position.UnitPrice <= 0)
            {
                return false;
            }

            var apiKey = CryptoHelper.Decrypt(user.Parameter.ApiKey, user.EmailHash);
            var apiSecret = CryptoHelper.Decrypt(user.Parameter.ApiSecret, user.EmailHash);

            var simulationSell = this.repositories.SimulationRepository.SimulateSell(user.Parameter, position.UnitPrice, position.QuantityExecuted);
            if (simulationSell == null)
            {
                return false;
            }

            var requestCreateOrder = new RequestCreateOrder()
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret,
                MarketSymbol = market.Symbol,
                Quantity = simulationSell.Quantity.TruncateCrypto().ToString(),
                Side = Side.Sell.ToString().ToUpper(),
                UnitPrice = simulationSell.UnitPrice.ToString(),
            };
            var responseNewOrder = await this.foxbitRepository.CreateOrder(requestCreateOrder);
            if (responseNewOrder == null)
            {
                return false;
            }

            var newPosition = requestCreateOrder.ToPosition(responseNewOrder, user.Id, market.Id, Side.Sell);
            newPosition.CircuitId = position.CircuitId;
            await this.repositories.PositionRepository.Save(newPosition);

            return true;
        }

        public Task<IEnumerable<User>> GetUsersToRunCircuit()
        => this.repositories.UserRepository.GetUsersToRunCircuit();
    }
}
