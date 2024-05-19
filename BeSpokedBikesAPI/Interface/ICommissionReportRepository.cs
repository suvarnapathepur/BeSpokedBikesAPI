using BeSpokedBikesAPI.Model;

namespace BeSpokedBikesAPI.Interface
{
    public interface ICommissionReportRepository
    {
        Task<IEnumerable<QuarterlyCommissionReport>> GetQuarterlyCommissionReportAsync(int year);
    }
}
