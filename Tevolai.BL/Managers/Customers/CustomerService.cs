using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tevolai.BL.DTOs.CustomerDto;
using Tevolai.DAL.Entities;
using Tevolai.DAL.Repository.CustomerRepo;

namespace Tevolai.BL.Managers.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        //Add New Customer
        public async Task AddCustomerAsync(CustomerDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Country = dto.Country,
                Notes = dto.Notes,
                CompanyName = dto.CompanyName,
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddCustomerAsync(customer);
        }

        //Get Customer By Id
        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _repo.GetCustomerByIdAsync(id);
            if (customer == null || customer.IsDeleted)
                return null;
            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.Phone,
                CompanyName = customer.CompanyName,
                Country = customer.Country,
                Notes = customer.Notes,
                CreatedAt = customer.CreatedAt
            };
        }

        //Update Customer
        public async Task UpdateCustomerAsync(CustomerDto dto)
        {
            var customer = await _repo.GetCustomerByIdAsync(dto.Id);
            if (customer == null)
                throw new Exception("Customer not found");
            customer.FullName = dto.FullName;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.CompanyName = dto.CompanyName;
            customer.Country = dto.Country;
            customer.Notes = dto.Notes;
            await _repo.UpdateCustomerAsync(customer);
        }

        //Delete Customer
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _repo.GetCustomerByIdAsync(id);
            if (customer == null)
                throw new Exception("Customer not found");
            customer.IsDeleted = true;
            await _repo.UpdateCustomerAsync(customer);
        }

        //Get All Customers
        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _repo.GetAllCustomersAsync();
            return customers
                .Where(c => !c.IsDeleted)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Email = c.Email,
                    Phone = c.Phone,
                    CompanyName = c.CompanyName,
                    Country = c.Country,
                    Notes = c.Notes,
                    CreatedAt = c.CreatedAt
                })
                .ToList();
        }

        //Search Customers
        public async Task<List<CustomerDto>> SearchCustomersAsync(string searchTerm)
        {
            var customers = await _repo.SearchCustomersAsync(searchTerm);
            return customers
                .Where(c => !c.IsDeleted)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Email = c.Email,
                    Phone = c.Phone,
                    CompanyName = c.CompanyName,
                    Country = c.Country,
                    Notes = c.Notes,
                    CreatedAt = c.CreatedAt
                })
                .ToList();
        }

        //Get Customers Count
        public async Task<int> GetCustomersCountAsync()
        {
            var customers = await _repo.GetAllCustomersAsync();
            return customers.Count;
        }
    }
}