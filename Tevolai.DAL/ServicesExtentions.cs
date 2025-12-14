using Microsoft.Extensions.DependencyInjection;
using Tevolai.DAL.Repository.ContactMessageRepo;
using Tevolai.DAL.Repository.CustomerRepo;
using Tevolai.DAL.Repository.EmailCredentialRepo;
using Tevolai.DAL.Repository.ExpenseRepo;
using Tevolai.DAL.Repository.RevenueRepo;
using Tevolai.DAL.Repository.ServicesOnDashBoardRepo;
using Tevolai.DAL.Repository.ServicesOnMainRepo;

namespace Tevolai.DAL
{
    public static class ServicesExtentions
    {
        public static void AddDALServices(this IServiceCollection services)
        {
            // Add DAL related services here
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmailCredentialRepository, EmailCredentialRepository>();
            services.AddScoped<IServicesOnDashBoardRepository, ServicesOnDashBoardRepository>();
            services.AddScoped<IRevenueRepository, RevenueRepository>();
            services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
            services.AddScoped<IServicesOnMainRepository, ServicesOnMainRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
        }
    }
}