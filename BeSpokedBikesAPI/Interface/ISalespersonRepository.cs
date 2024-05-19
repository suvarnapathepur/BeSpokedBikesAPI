using BeSpokedBikesAPI.Model;

namespace BeSpokedBikesAPI.Interface
{
    public interface ISalespersonRepository
    {
        Task<IEnumerable<Salesperson>> GetAll();
        Task<Salesperson> GetById(int id);
        //Task Add(Salesperson product);
        Task Update(Salesperson product);
        //Task Delete(int id);
    }
}
