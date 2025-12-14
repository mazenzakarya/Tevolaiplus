using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tevolai.DAL.Context;
using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TevolaiDbContext db;

        public CustomerRepository(TevolaiDbContext db)
        {
            this.db = db;
        }

        // Add methods for customer data access here
        public async Task AddCustomerAsync(Customer customer)
        {
            await db.Customers.AddAsync(customer);
            await db.SaveChangesAsync();
        }

        //get customer by id
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await db.Customers.FindAsync(id);
        }

        //update customer
        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await GetCustomerByIdAsync(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.FullName = customer.FullName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.CompanyName = customer.CompanyName;
                existingCustomer.Country = customer.Country;
                existingCustomer.Notes = customer.Notes;
            }
            await db.SaveChangesAsync();
        }

        //soft delete customer
        public async Task SoftDeleteCustomerAsync(int id)
        {
            var customer = await GetCustomerByIdAsync(id);
            if (customer != null)
            {
                customer.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        //get all customers with pagination
        public async Task<List<Customer>> GetAllCustomersAsync(int pageNumber, int pageSize)
        {
            return await db.Customers
                .Where(c => !c.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        //get total count of customers
        public async Task<int> GetTotalCustomersCountAsync()
        {
            return await db.Customers.CountAsync(c => !c.IsDeleted);
        }

        //get all customers
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await db.Customers
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        //search customers by name or email
        public async Task<List<Customer>> SearchCustomersAsync(string searchTerm)
        {
            return await db.Customers
                .Where(c => !c.IsDeleted &&
                            (c.FullName.Contains(searchTerm) || c.Email.Contains(searchTerm)))
                .ToListAsync();
        }
    }
}