using BeSpokedBikesAPI.Model;

namespace BeSpokedBikesAPI.Interface
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAll();
        Task<Sale> GetById(int id);
        Task Add(Sale product);
        Task Update(Sale product);
        Task Delete(int id);
    }
}
