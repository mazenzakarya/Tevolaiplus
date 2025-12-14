using Tevolai.BL.DTOs.CustomerDto;

namespace Tevolai.BL.Managers.Customers
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(CustomerDto dto);
        Task DeleteCustomerAsync(int id);
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<int> GetCustomersCountAsync();
        Task<List<CustomerDto>> SearchCustomersAsync(string searchTerm);
        Task UpdateCustomerAsync(CustomerDto dto);
    }
}