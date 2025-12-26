using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tevolai.DAL.Context;
using Tevolai.DAL.Entities;
using Tevolai.BL.Managers.EmailCredentials;

namespace Tevolai.APIs.Helpers
{
    public class DatabaseSeeder
    {
        private readonly TevolaiDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EncryptionService _encryptionService;

        public DatabaseSeeder(
            TevolaiDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            EncryptionService encryptionService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _encryptionService = encryptionService;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            // 1?? Seed Roles
            await SeedRolesAsync();

            // 2?? Seed Users
            var users = await SeedUsersAsync();

            // Users references
            var adminUser = users.First(u => u.UserName == "admin@tevolai.com");
            var customerUser1 = users.First(u => u.UserName == "johndoe1");
            var customerUser2 = users.First(u => u.UserName == "johndoe2");

            // 3?? Seed Customers
            var customers = await SeedCustomersAsync();
            var customer1 = customers[0];
            var customer2 = customers[1];

            // 4?? Seed Services on Main
            await SeedServicesOnMainAsync();

            // 5?? Seed Services on Dashboard (linked with Users)
            await SeedServicesOnDashboardAsync(customer1, customer2, adminUser, customerUser1, customerUser2);

            // 6?? Seed Email Credentials
            await SeedEmailCredentialsAsync(customer1, customer2);

            // 7?? Seed Expenses
            await SeedExpensesAsync();

            // 9?? Seed Contact Messages
            await SeedContactMessagesAsync();
        }

        private async Task SeedRolesAsync()
        {
            var roles = new[] { "Admin", "Customer" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task<List<ApplicationUser>> SeedUsersAsync()
        {
            if (await _userManager.Users.AnyAsync())
                return await _userManager.Users.ToListAsync();

            var adminUser = new ApplicationUser
            {
                UserName = "admin@tevolai.com",
                Email = "admin@tevolai.com",
                FullName = "Admin User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            var customerUser1 = new ApplicationUser
            {
                UserName = "johndoe1",
                Email = "johndoe1@example.com",
                FullName = "John Doe",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            var customerUser2 = new ApplicationUser
            {
                UserName = "johndoe2",
                Email = "johndoe2@example.com",
                FullName = "John Doe2",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userManager.CreateAsync(adminUser, "Admin@123");
            await _userManager.CreateAsync(customerUser1, "Password@123");
            await _userManager.CreateAsync(customerUser2, "Password@123");

            await _userManager.AddToRoleAsync(adminUser, "Admin");
            await _userManager.AddToRoleAsync(customerUser1, "Customer");
            await _userManager.AddToRoleAsync(customerUser2, "Customer");

            return new List<ApplicationUser> { adminUser, customerUser1, customerUser2 };
        }

        private async Task<List<Customer>> SeedCustomersAsync()
        {
            if (await _context.Customers.AnyAsync())
                return await _context.Customers.Where(c => !c.IsDeleted).ToListAsync();

            var customers = new List<Customer>
            {
                new Customer
                {
                    FullName = "John Doe",
                    Email = "john.doe@example.com",
                    Phone = "+1-555-0101",
                    CompanyName = "Tech Solutions Inc.",
                    Country = "USA",
                    Notes = "Premium client - High priority",
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    IsDeleted = false
                },
                new Customer
                {
                    FullName = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Phone = "+1-555-0102",
                    CompanyName = "Digital Marketing Pro",
                    Country = "Canada",
                    Notes = "Regular client",
                    CreatedAt = DateTime.UtcNow.AddDays(-25),
                    IsDeleted = false
                }
            };

            await _context.Customers.AddRangeAsync(customers);
            await _context.SaveChangesAsync();

            return customers;
        }

        private async Task SeedServicesOnMainAsync()
        {
            if (await _context.ServicesOnMains.AnyAsync()) return;

            var services = new List<ServicesOnMain>
            {
                new ServicesOnMain
                {
                    Title = "Website Development",
                    Description = "Professional website development using modern technologies",
                    Price = 5000.00m,
                    ImageUrl = "/images/services/website-dev.jpg",
                    Category = "Development",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    IsDeleted = false
                },
                new ServicesOnMain
                {
                    Title = "E-Commerce Solutions",
                    Description = "Complete e-commerce platform development",
                    Price = 8000.00m,
                    ImageUrl = "/images/services/ecommerce.jpg",
                    Category = "E-Commerce",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-12),
                    IsDeleted = false
                }
            };

            await _context.ServicesOnMains.AddRangeAsync(services);
            await _context.SaveChangesAsync();
        }

        private async Task SeedServicesOnDashboardAsync(Customer customer1, Customer customer2, ApplicationUser adminUser, ApplicationUser customerUser1, ApplicationUser customerUser2)
        {
            if (await _context.ServicesOnDashBoards.AnyAsync()) return;

            var services = new List<ServicesOnDashBoard>
            {
                new ServicesOnDashBoard
                {
                    CustomerId = customer1.Id,
                    ServiceName = "John's WordPress Site",
                    Domain = "johndoe.com",
                    ApplicationUserId = adminUser.Id,
                    UserName = "johndoe_admin",
                    CMS = WebsiteCMS.WordPress,
                    HostingServer = "Bluehost",
                    Status = WebsiteStatus.Active,
                    Notes = "Production site - High traffic",
                    PaidStatus = PaidStatus.paid,
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    IsDeleted = false
                },
                new ServicesOnDashBoard
                {
                    CustomerId = customer2.Id,
                    ServiceName = "Jane's E-Commerce Platform",
                    Domain = "janeshop.com",
                    ApplicationUserId = customerUser2.Id,
                    UserName = "jane_admin",
                    CMS = WebsiteCMS.DotNet,
                    HostingServer = "Azure",
                    Status = WebsiteStatus.Active,
                    Notes = "E-commerce platform with payment integration",
                    PaidStatus = PaidStatus.unpaid,
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    IsDeleted = false
                }
            };

            await _context.ServicesOnDashBoards.AddRangeAsync(services);
            await _context.SaveChangesAsync();
        }

        private async Task SeedEmailCredentialsAsync(Customer customer1, Customer customer2)
        {
            if (await _context.EmailCredentials.AnyAsync()) return;

            var credentials = new List<EmailCredential>
            {
                new EmailCredential
                {
                    CustomerId = customer1.Id,
                    EmailAddress = "admin@johndoe.com",
                    EncryptedPassword = _encryptionService.Encrypt("SecurePass123!"),
                    UserName = "johndoe_admin",
                    HostingPlatform = "Bluehost",
                    Notes = "Main admin email for johndoe.com",
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    IsDeleted = false
                },
                new EmailCredential
                {
                    CustomerId = customer2.Id,
                    EmailAddress = "support@janeshop.com",
                    EncryptedPassword = _encryptionService.Encrypt("ShopPass456!"),
                    UserName = "jane_support",
                    HostingPlatform = "Azure",
                    Notes = "Support email for e-commerce platform",
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    IsDeleted = false
                }
            };

            await _context.EmailCredentials.AddRangeAsync(credentials);
            await _context.SaveChangesAsync();
        }

        private async Task SeedExpensesAsync()
        {
            if (await _context.Expenses.AnyAsync()) return;

            var expenses = new List<Expense>
            {
                new Expense { Title = "Server Hosting", Amount = 150.00m, Category = "Infrastructure", Date = DateTime.UtcNow.AddDays(-5), Notes = "Monthly hosting fee", IsDeleted = false },
                new Expense { Title = "Domain Renewal", Amount = 15.00m, Category = "Domain", Date = DateTime.UtcNow.AddDays(-3), Notes = "Annual domain renewal", IsDeleted = false },
                new Expense { Title = "SSL Certificate", Amount = 50.00m, Category = "Security", Date = DateTime.UtcNow.AddDays(-1), Notes = "SSL certificate purchase", IsDeleted = false }
            };

            await _context.Expenses.AddRangeAsync(expenses);
            await _context.SaveChangesAsync();
        }

        private async Task SeedContactMessagesAsync()
        {
            if (await _context.ContactMessages.AnyAsync()) return;

            var messages = new List<ContactMessage>
            {
                new ContactMessage { FullName = "Sarah Johnson", Email = "sarah.johnson@example.com", Subject = "Website Inquiry", Message = "I'm interested in your website development services.", CreatedAt = DateTime.UtcNow.AddDays(-7), IsRead = false },
                new ContactMessage { FullName = "Michael Brown", Email = "michael.brown@example.com", Subject = "Mobile App Dev", Message = "Looking for mobile app development.", CreatedAt = DateTime.UtcNow.AddDays(-5), IsRead = true }
            };

            await _context.ContactMessages.AddRangeAsync(messages);
            await _context.SaveChangesAsync();
        }
    }
}