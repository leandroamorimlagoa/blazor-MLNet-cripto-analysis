//using System.Collections;
//using System.Collections.Concurrent;
//using LagoaTrading.Data.Repository.Contexts;
//using LagoaTrading.Data.Service.Extensions;
//using LagoaTrading.Domain.Core.Tools;
//using LagoaTrading.Domain.Entities;
//using LagoaTrading.Domain.Interfaces.FoxbitServices;
//using LagoaTrading.Domain.Objects.FoxbitObjects.Requests;
//using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;
//using LagoaTrading.Shared.Core;
//using LagoaTrading.Shared.Enumerators;
//using LagoaTrading.Shared.Extensions;
//using Microsoft.EntityFrameworkCore;

//namespace LagoaTrading.Server.IntegrationManagers
//{
//    public class FoxbitManager
//    {
//        private readonly IFoxbitHttpClient client;
//        private readonly IPositionOrderHandler positionOrderHandler;
//        private readonly ICircuitHandler circuitHandler;
//        private readonly IParameterHandler parameterHandler;
//        private ConcurrentQueue<RequestCandlesticks> candlestickRequests = new ConcurrentQueue<RequestCandlesticks>();
//        private Dictionary<string, long> MarketIds = new Dictionary<string, long>();
//        private LagoaTradingContext context { get; set; }

//        public FoxbitManager(IFoxbitHttpClient client
//                            , LagoaTradingContext context
//                            , IPositionOrderHandler positionOrderHandler
//                            , ICircuitHandler circuitHandler
//                            , IParameterHandler parameterHandler)
//        {
//            this.client = client;
//            this.context = context;
//            this.positionOrderHandler = positionOrderHandler;
//            this.circuitHandler = circuitHandler;
//            this.parameterHandler = parameterHandler;
//        }

//        public async Task ProcessPrivate()
//        {
//        }

//        public async Task ProcessUserPositions(long UserId = 0)
//        {
//            var users = await context.User.Include(u => u.Parameter)
//                                .Where(u => (UserId == 0 || u.Id == UserId)
//                                            && u.Status == UserStatus.Active
//                                            && (u.Parameter.CircuitCommand == CircuitCommand.Start || u.Parameter.CircuitCommand == CircuitCommand.Executing))
//                                .ToListAsync();

//            foreach (var user in users)
//            {
//                await UpdateUserPositions(user, user.Parameter);
//            }
//        }

//        public async Task ProcessBaseRegisters()
//        {
//            await this.SyncCurrencies();
//            await this.SyncMarkets();
//            await this.SyncCandlesticks();
//        }

//        public async Task PositionOrderMonitoring()
//        {
//            var positions = await context.Position
//                                                .Include(p => p.Market)
//                                                .Include(p => p.Circuit)
//                                                .Include(p => p.User)
//                                                .ThenInclude(u => u.Parameter)
//                                                .Where(p => p.State != State.Cancelled
//                                                             && p.State != State.Filled)
//                                                .ToListAsync();
//            if (!positions.Any())
//            {
//                return;
//            }

//            await this.SyncCandlesticks(true);

//            foreach (var position in positions)
//            {
//                var apiKey = CryptoHelper.Decrypt(position.User.Parameter.ApiKey, position.User.EmailHash);
//                var apiSecret = CryptoHelper.Decrypt(position.User.Parameter.ApiSecret, position.User.EmailHash);

//                var request = new RequestOrderStatusByID(apiKey, apiSecret, position.OrderId.ToString());
//                var response = await client.Get<ResponseOrderStatusByID>(request);
//                if (response != null)
//                {
//                    await UpdatePosition(response, position);

//                    if (response.GetState() != State.Filled || !position.CircuitId.HasValue)
//                    {
//                        continue;
//                    }

//                    Circuit circuit = await this.circuitHandler.Get(position.CircuitId.Value);
//                    if (response.GetSide() == Side.Buy)
//                    {
//                        await this.positionOrderHandler.CreatePositionOrderToSell(position.User, position.User.Parameter, position.Market, position.UnitPrice, position.QuantityExecuted, circuit.Id);
//                    }
//                    else
//                    {
//                        // TODO: Update User Parameters Values (Absolute, Avaliable)
//                        position.User.Parameter.AvaliableValue += (position.QuantityExecuted * position.UnitPrice).TruncateCurrency();
//                        await this.parameterHandler.Update(position.User.Parameter);
//                        await this.circuitHandler.EndCircuit(circuit);
//                        await this.UpdateUserAccount(position.User, position.User.Parameter);

//                        if (circuit.CircuitType == CircuitType.Single
//                            || position.User.Parameter.CircuitCommand == CircuitCommand.Stoping)
//                        {
//                            await this.parameterHandler.RegisterCircuitStopped(position.User.Parameter);
//                            continue;
//                        }

//                        if (position.User.Parameter.CircuitCommand == CircuitCommand.Executing)
//                        {
//                            await this.circuitHandler.StartContinuousCircuit(position.User);

//                        }
//                    }
//                }
//            }
//        }

//        private async Task UpdatePosition(ResponseOrderStatusByID response, Position position)
//        {
//            position.State = response.GetState();
//            position.ResponsePrice = Convert.ToDecimal(response.Price);
//            position.ResponsePriceAVG = Convert.ToDecimal(response.PriceAvg);
//            position.Quantity = Convert.ToDecimal(response.Quantity);
//            position.QuantityExecuted = Convert.ToDecimal(response.QuantityExecuted);
//            // TODO buscar a data real de finalização da posição. Ver Trades Info
//            position.Executed = DateTime.UtcNow;
//            position.TradeCounts = response.TradesCount;

//            context.Update(position);
//            context.SaveChanges();
//        }

//        private async Task UpdateUserPositions(User user, Parameter parameter)
//        {
//            var apiKey = CryptoHelper.Decrypt(parameter.ApiKey, user.EmailHash);
//            var apiSecret = CryptoHelper.Decrypt(parameter.ApiSecret, user.EmailHash);

//            var userOrders = new RequestUserOrders(apiKey, apiSecret);
//            var result = await client.Get<ResponseUserOrdersDataSet>(userOrders);
//            var markets = context.Market.ToDictionary(m => m.Symbol, m => m.Id);

//            if (result == null) { return; }

//            foreach (var item in result.Data)
//            {
//                if (item == null
//                    || item.GetState() == State.Cancelled)
//                { continue; }

//                if (!markets.ContainsKey(item.MarketSymbol)) { continue; }

//                var marketId = markets[item.MarketSymbol];
//                var userOrder = context.Position.FirstOrDefault(u => u.OrderId == item.id);
//                if (userOrder == null)
//                {
//                    userOrder = new Position() { OrderId = item.id, MarketId = marketId, ClientOrderId = item.ClientOrderId };
//                }
//                userOrder.UserId = parameter.UserId;
//                userOrder.ClientOrderId = item.ClientOrderId;
//                userOrder.Identifier = item.Identifier;
//                userOrder.State = item.GetState();
//                userOrder.Side = item.GetSide();
//                userOrder.UnitPrice = Convert.ToDecimal(item.Price);
//                userOrder.Quantity = Convert.ToDecimal(item.Quantity);
//                userOrder.QuantityExecuted = Convert.ToDecimal(item.QuantityExecuted);
//                userOrder.CreatedAt = Convert.ToDateTime(item.CreatedAt);
//                userOrder.OrderType = item.GetItemType();

//                if (userOrder.Id == 0)
//                {
//                    context.Add(userOrder);
//                }
//                else
//                {
//                    context.Update(userOrder);
//                }
//                context.SaveChanges();
//            }
//        }

//        private async Task UpdateUserAccount(User user, Parameter parameter)
//        {
//            var apiKey = CryptoHelper.Decrypt(parameter.ApiKey, user.EmailHash);
//            var apiSecret = CryptoHelper.Decrypt(parameter.ApiSecret, user.EmailHash);

//            var memberAccount = new RequestMemberAccount(apiKey, apiSecret);
//            var result = await client.Get<MemberAccountDataSet>(memberAccount);
//            if (result == null) { return; }

//            var list = result.Data.Where(u => u.Balance > 0 || u.BalanceAvailable > 0 || u.BalanceBlocked > 0).ToList();
//            foreach (var item in list)
//            {

//                var currency = context.Currency.FirstOrDefault(c => c.Symbol == item.CurrencySymbol);
//                if (currency == null) { continue; }

//                var userAccount = context.UserAccount.FirstOrDefault(u => u.UserId == user.Id && u.CurrencyId == currency.Id);
//                if (userAccount == null)
//                {
//                    userAccount = new UserAccount() { CurrencyId = currency.Id, UserId = user.Id };
//                }

//                userAccount.Balance = item.Balance;
//                userAccount.BalanceAvailable = item.BalanceAvailable;
//                userAccount.BalanceBlocked = item.BalanceBlocked;

//                if (userAccount.Id == 0)
//                {
//                    context.Add(userAccount);
//                }
//                else
//                {
//                    context.Update(userAccount);
//                }
//                context.SaveChanges();
//            }
//            var userAccountSync = SyncControl.UserAccountsName.Replace("{userid}", user.Id.ToString());
//            await RegisterSync(userAccountSync);
//        }
//}
