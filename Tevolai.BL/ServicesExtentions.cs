using Microsoft.Extensions.DependencyInjection;
using Tevolai.BL.Managers.Auth;
using Tevolai.BL.Managers.ContactMessages;
using Tevolai.BL.Managers.Customers;
using Tevolai.BL.Managers.EmailCredentials;
using Tevolai.BL.Managers.Expenses;
using Tevolai.BL.Managers.Revenues;
using Tevolai.BL.Managers.ServicesOnDashBoards;
using Tevolai.BL.Managers.ServicesOnMains;

namespace Tevolai.BL
{
    public static class ServicesExtentions
    {
        public static void AddBLServices(this IServiceCollection services)
        {
            //services.AddScoped<IBlogPostManager, BlogPostManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IContactMessageService, ContactMessageService>();
            services.AddScoped<IEmailCredentialService, EmailCredentialService>();
            services.AddScoped<EncryptionService>();
            services.AddScoped<IRevenueService, RevenueService>();
            services.AddScoped<IServicesOnDashBoardService, ServicesOnDashBoardService>();
            services.AddScoped<IServicesOnMainService, ServicesOnMainService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}