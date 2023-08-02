//using LagoaTrading.Data.Repository.Contexts;
//using LagoaTrading.Data.Service.Requests;
//using LagoaTrading.Data.Service.Responses;
//using LagoaTrading.Domain.Entities;
//using LagoaTrading.Domain.Interfaces.FoxbitServices;
//using LagoaTrading.Domain.Interfaces.Handlers;
//using LagoaTrading.Shared.Core;
//using LagoaTrading.Shared.Enumerators;
//using Microsoft.EntityFrameworkCore;

//namespace LagoaTrading.Server.Core.Handlers
//{
//    public class PositionOrderHandler : IPositionOrderHandler
//    {
//        private readonly IAnalysisHandler analysisHandler;
//        private readonly IFoxbitHttpClient foxbitHttpClient;
//        private readonly IParameterHandler parameterHandler;
//        private readonly LagoaTradingContext context;

//        public PositionOrderHandler(IAnalysisHandler analysisHandler
//                                    , IFoxbitHttpClient foxbitHttpClient
//                                    , IParameterHandler parameterHandler
//                                    , LagoaTradingContext context)
//        {
//            this.analysisHandler = analysisHandler;
//            this.foxbitHttpClient = foxbitHttpClient;
//            this.parameterHandler = parameterHandler;
//            this.context = context;
//        }

//        public async Task<Position> CreatePositionOrderToBuy(User user, Parameter parameter)
//        {
//            if (user == null || parameter == null)
//            {
//                return null;
//            }

//            var analysis = await analysisHandler.GetAnalysis(user);
//            if (analysis == null)
//            {
//                return null;
//            }

//            var market = await context.Market.FirstOrDefaultAsync(m => m.Id == analysis.MarketId);
//            if (market == null)
//            {
//                return null;
//            }

//            var simulationHandler = new SimulationHandler(user, user.Parameter, market, foxbitHttpClient);
//            var simulation = await simulationHandler.Handle();
//            if (simulation == null)
//            {
//                return null;
//            }

//            var apiKey = CryptoHelper.Decrypt(user.Parameter.ApiKey, user.EmailHash);
//            var apiSecret = CryptoHelper.Decrypt(user.Parameter.ApiSecret, user.EmailHash);

//            var requestNewOrder = new RequestCreateOrder()
//            {
//                ApiSecret = apiSecret,
//                ApiKey = apiKey,
//                Side = Side.Buy,
//                MarketSymbol = analysis.Symbol,
//                Quantity = simulation.QuantityToBuy.ToString(),
//                Price = simulation.UnitPriceToBuy.ToString(),
//            };

//            var responseNewOrder = await foxbitHttpClient.Post<ResponseCreateOrder>(requestNewOrder);
//            if (responseNewOrder == null)
//            {
//                return null;
//            }

//            var position = new Position
//            {
//                UserId = user.Id,
//                MarketId = analysis.MarketId,
//                Side = Side.Buy,
//                OrderType = requestNewOrder.Type,
//                OrderId = responseNewOrder.Id,
//                ClientOrderId = responseNewOrder.ClientOrderId,
//                Quantity = Convert.ToDecimal(requestNewOrder.Quantity),
//                UnitPrice = Convert.ToDecimal(requestNewOrder.Price),
//                State = State.Registered,
//                CreatedAt = DateTime.UtcNow,
//                Created = DateTime.UtcNow,
//            };

//            parameter.AvaliableValue -= simulation.TotalPriceToBuy;
//            await parameterHandler.Update(parameter);

//            // register position to buy
//            await context.Position.AddAsync(position);
//            await context.SaveChangesAsync();
//            return position;
//        }

//        public async Task<Position> CreatePositionOrderToSell(User user, Parameter parameter, Market market, decimal basePrice, decimal quantity, long circuitId)
//        {
//            if (user == null
//                || parameter == null
//                || market == null
//                || basePrice <= 0)
//            {
//                return null;
//            }

//            var simulationHandler = new SimulationHandler(user, user.Parameter, market, foxbitHttpClient);
//            var simulation = simulationHandler.HandleSellPosition(basePrice, quantity);
//            if (simulation == null)
//            {
//                return null;
//            }

//            var apiKey = CryptoHelper.Decrypt(user.Parameter.ApiKey, user.EmailHash);
//            var apiSecret = CryptoHelper.Decrypt(user.Parameter.ApiSecret, user.EmailHash);

//            var requestNewOrder = new RequestCreateOrder()
//            {
//                ApiSecret = apiSecret,
//                ApiKey = apiKey,
//                Side = Side.Sell,
//                MarketSymbol = market.Symbol,
//                Quantity = quantity.ToString(),
//                Price = basePrice.ToString(),
//            };

//            var responseNewOrder = await foxbitHttpClient.Post<ResponseCreateOrder>(requestNewOrder);
//            if (responseNewOrder == null)
//            {
//                return null;
//            }

//            var position = new Position
//            {
//                UserId = user.Id,
//                MarketId = market.Id,
//                CircuitId = circuitId,
//                Side = Side.Buy,
//                OrderType = requestNewOrder.Type,
//                OrderId = responseNewOrder.Id,
//                ClientOrderId = responseNewOrder.ClientOrderId,
//                Quantity = Convert.ToDecimal(requestNewOrder.Quantity),
//                UnitPrice = Convert.ToDecimal(requestNewOrder.Price),
//                State = State.Registered,
//                CreatedAt = DateTime.UtcNow,
//                Created = DateTime.UtcNow,
//            };

//            // register position to buy
//            await context.Position.AddAsync(position);
//            await context.SaveChangesAsync();
//            return position;
//        }
//    }
//}
