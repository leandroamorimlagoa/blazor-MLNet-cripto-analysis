//using LagoaTrading.Data.Repository.Contexts;
//using LagoaTrading.Domain.Entities;
//using LagoaTrading.Domain.Interfaces.Handlers;
//using LagoaTrading.Shared.Enumerators;

//namespace LagoaTrading.Server.Core.Handlers
//{
//    public class ParameterHandler : IParameterHandler
//    {
//        private readonly LagoaTradingContext context;

//        public ParameterHandler(LagoaTradingContext context)
//        {
//            this.context = context;
//        }

//        public async Task RegisterCircuitStopped(Parameter parameter)
//        {
//            parameter.CircuitCommand = CircuitCommand.Stopped;
//            context.Parameter.Update(parameter);
//            await context.SaveChangesAsync();
//        }

//        public async Task Update(Parameter parameter)
//        {
//            context.Parameter.Update(parameter);
//            await context.SaveChangesAsync();
//        }
//    }
//}
