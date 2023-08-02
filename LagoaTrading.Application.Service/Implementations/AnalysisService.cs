using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.ApiResultObjects;

namespace LagoaTrading.Application.Service.Implementations
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IApplicationRepositories applicationRepositories;

        public AnalysisService(IApplicationRepositories applicationRepositories)
        {
            this.applicationRepositories = applicationRepositories;
        }

        public async Task<IEnumerable<Analysis>> GetQuickAnalysis(string userHash)
        {
            (User user, Parameter parameter) = await this.applicationRepositories.UserRepository.GetUserByRollingHashWithParameter(userHash);
            if (user == null || parameter == null)
            {
                return default;
            }

            var analysisList = await this.applicationRepositories.AnalysisRepository.GetAnalysisList(user, parameter, rows: 10, fromLastHours: 24);

            return analysisList;
        }
    }
}
