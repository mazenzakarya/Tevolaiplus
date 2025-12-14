using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.CustomerRepo
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(Customer customer);

        Task<List<Customer>> GetAllCustomersAsync();

        Task<List<Customer>> GetAllCustomersAsync(int pageNumber, int pageSize);

        Task<Customer?> GetCustomerByIdAsync(int id);

        Task<int> GetTotalCustomersCountAsync();

        Task SoftDeleteCustomerAsync(int id);

        Task UpdateCustomerAsync(Customer customer);

        Task<List<Customer>> SearchCustomersAsync(string searchTerm);
    }
}