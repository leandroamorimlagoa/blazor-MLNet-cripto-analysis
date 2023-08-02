using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.ApiResultObjects;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface IAnalysisRepository
    {
        Task<Analysis?> GetAnalysis(User user, Parameter parameter, int fromLastHours);
        Task<IEnumerable<Analysis>> GetAnalysisList(User user, Parameter parameter, int rows, int fromLastHours);
    }
}
