using BeSpokedBikesAPI.Model;

namespace BeSpokedBikesAPI.Interface
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Discount>> GetAll();
        Task<Discount> GetById(int id);
        Task Add(Discount product);
        Task Update(Discount product);
        Task Delete(int id);
    }
}
