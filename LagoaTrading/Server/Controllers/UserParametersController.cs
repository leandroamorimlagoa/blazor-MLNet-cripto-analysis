using LagoaTrading.Domain.Core.Securities;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class UserParametersController : BaseController
    {
        public UserParametersController(IApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpGet("{hash}")]
        public async Task<IActionResult> Get(string hash)
        {
            if (string.IsNullOrEmpty(hash))
            {
                return BadRequest(hash);
            }

            CryptoHelper.DecryptWithSecret(hash, out string userHash, out string key);
            (User user, Parameter parameter) = await this.ApplicationService.UserService.GetUserByRollingHashWithParameter(userHash);
            if (parameter == default || user == null)
            {
                return BadRequest(hash);
            }

            var apiKey = CryptoHelper.Decrypt(parameter.ApiKey, user.EmailHash);
            var apiSecret = CryptoHelper.Decrypt(parameter.ApiSecret, user.EmailHash);

            var response = new UserParameterResponse(apiKey, apiSecret);
            response.TypeValue = parameter.TypeValue;
            response.TypeValue = parameter.TypeValue;
            response.ReferenceValue = parameter.ReferenceValue;
            response.ReferenceAbsoluteValue = parameter.ReferenceAbsoluteValue;
            response.OnlyPositiveCryptos = parameter.OnlyPositiveCryptos;
            response.MinimumCryptoValue = parameter.MinimumCryptoValue;
            response.MaximumCryptoValue = parameter.MaximumCryptoValue;
            response.PercentageToDecreaseToBuy = parameter.PercentageToDecreaseToBuy;
            response.PercentageToIncreaseToSell = parameter.PercentageToIncreaseToSell;
            response.AccountBalanceCurrency = parameter.AccountBalanceCurrency;
            response.AvaliableValue = parameter.AvaliableValue;

            return Ok(response);
        }

        [HttpPost("{hash}")]
        public async Task<IActionResult> Post([FromRoute] string hash, [FromBody] UserParameterRequest request)
        {
            if (string.IsNullOrEmpty(hash))
            {
                return BadRequest(hash);
            }

            if (!IsRequestValid(request))
            {
                return BadRequest(request);
            }

            CryptoHelper.DecryptWithSecret(hash, out string userHash, out string key);

            (User user, Parameter parameter) = await this.ApplicationService.UserService.GetUserByRollingHashWithParameter(userHash);
            if (user == default
                || parameter == default)
            {
                return BadRequest(hash);
            }

            var apiKey = request.GetApiKey();
            var apiSecret = request.GetApiSecret();
            var foxbitUser = await this.ApplicationService.FoxbitService.RequestUserInfo(apiKey, apiSecret);
            if (foxbitUser == null || foxbitUser.Disabled)
            {
                return BadRequest(foxbitUser);
            }

            var accountBalanceCurrency = await GetAccountBalance(apiKey, apiSecret, ConstantNamesServer.Currency.BrlSymbol);
            var openedPositions = await GetActivePositionsFromUser(user.Id);
            var absoluteReferenceValue = request.TypeValue == ParameterTypeValue.AbsoluteValue
                                                            ? request.ReferenceAbsoluteValue
                                                            : ((request.ReferenceValue * accountBalanceCurrency) / 100);

            parameter.TypeValue = request.TypeValue;
            parameter.ReferenceValue = request.ReferenceValue;
            parameter.AccountBalanceCurrency = accountBalanceCurrency;
            parameter.ReferenceAbsoluteValue = absoluteReferenceValue;
            parameter.AvaliableValue = absoluteReferenceValue - openedPositions;
            parameter.OnlyPositiveCryptos = request.OnlyPositiveCryptos;
            parameter.MinimumCryptoValue = request.MinimumCryptoValue;
            parameter.MaximumCryptoValue = request.MaximumCryptoValue;
            parameter.PercentageToDecreaseToBuy = request.PercentageToDecreaseToBuy;
            parameter.PercentageToIncreaseToSell = request.PercentageToIncreaseToSell;
            parameter.ApiSecret = CryptoHelper.Encrypt(apiSecret, user.EmailHash);
            parameter.ApiKey = CryptoHelper.Encrypt(apiKey, user.EmailHash);

            this.ApplicationService.UserService.UpdateParameter(parameter);

            return Ok();
        }

        private async Task<decimal> GetActivePositionsFromUser(long userId)
        => (await this.ApplicationService.PositionService.GetActivePositionsFromUser(userId))
                                                        .Sum(p => (p.UnitPrice * p.Quantity));

        private bool IsRequestValid(UserParameterRequest request)
        {
            var result = true;
            if (request == null
                || string.IsNullOrEmpty(request.ApiKey)
                || string.IsNullOrEmpty(request.ApiSecret)
                || request.ReferenceValue <= 0
                || request.ReferenceValue > 100
                || request.ReferenceAbsoluteValue <= 0
                || request.MinimumCryptoValue <= 0
                || request.MaximumCryptoValue <= 0
                || request.MinimumCryptoValue > request.MaximumCryptoValue
                || request.PercentageToDecreaseToBuy < 0
                || request.PercentageToIncreaseToSell < 0)
            {
                result = false;
            }

            return result;
        }
        private async Task<decimal> GetAccountBalance(string apiKey, string apiSecret, string symbol)
        {
            var result = await this.ApplicationService.FoxbitService.RequestMemberAccount(apiKey, apiSecret);
            var accountBalance = result.FirstOrDefault(x => x.CurrencySymbol == symbol);
            if (accountBalance == null)
            {
                return 0;
            }
            return accountBalance.Balance;
        }
    }
}
