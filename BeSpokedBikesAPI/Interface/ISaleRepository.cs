using BeSpokedBikesAPI.Model;

namespace BeSpokedBikesAPI.Interface
{
    public interface ISaleRepository
    {
        Task<IEnumerable<SaleDetailsDto>> GetSalesAsync(DateTime? startDate, DateTime? endDate);
        Task CreateSaleAsync(Sale sale);
    }
}
