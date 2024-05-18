using BeSpokedBikesAPI.Model;

namespace BeSpokedBikesAPI.Interface
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task Add(Customer product);
        Task Update(Customer product);
        Task Delete(int id);
    }
}
