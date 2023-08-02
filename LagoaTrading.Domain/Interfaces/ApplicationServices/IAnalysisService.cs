using LagoaTrading.Shared.ApiResultObjects;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface IAnalysisService
    {
        Task<IEnumerable<Analysis>> GetQuickAnalysis(string userHash);
    }
}
