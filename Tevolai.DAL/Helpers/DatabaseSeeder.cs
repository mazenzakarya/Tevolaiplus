using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tevolai.DAL.Context;
using Tevolai.DAL.Entities;
using Tevolai.BL.Managers.EmailCredentials;

namespace Tevolai.DAL.Helpers
{
    public class DatabaseSeeder
    {
        private readonly TevolaiDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EncryptionService _encryptionService;

        public DatabaseSeeder(
            TevolaiDbContext context,
            UserManager<ApplicationUser> userManager,
            EncryptionService encryptionService)
        {
            _context = context;
            _userManager = userManager;
            _encryptionService = encryptionService;
        }

        public async Task SeedAsync()
        {
            // Ensure database is created
            await _context.Database.EnsureCreatedAsync();

            // Seed Users
            await SeedUsersAsync();

            // Seed Customers
            await SeedCustomersAsync();

            // Seed Services on Main
            await SeedServicesOnMainAsync();

            // Seed Services on Dashboard
            await SeedServicesOnDashboardAsync();

            // Seed Email Credentials
            await SeedEmailCredentialsAsync();

            // Seed Expenses
            await SeedExpensesAsync();

            // Seed Revenues
            await SeedRevenuesAsync();

            // Seed Contact Messages
            await SeedContactMessagesAsync();
        }

        private async Task SeedUsersAsync()
        {
            if (await _userManager.Users.AnyAsync())
                return;

            var adminUser = new ApplicationUser
            {
                UserName = "admin@tevolai.com",
                Email = "admin@tevolai.com",
                FullName = "Admin User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userManager.CreateAsync(adminUser, "Admin@123");
        }

        private async Task SeedCustomersAsync()
        {
            if (await _context.Customers.AnyAsync())
                return;

            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
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
                    Id = 2,
                    FullName = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Phone = "+1-555-0102",
                    CompanyName = "Digital Marketing Pro",
                    Country = "Canada",
                    Notes = "Regular client",
                    CreatedAt = DateTime.UtcNow.AddDays(-25),
                    IsDeleted = false
                },
                new Customer
                {
                    Id = 3,
                    FullName = "Ahmed Hassan",
                    Email = "ahmed.hassan@example.com",
                    Phone = "+20-100-123-4567",
                    CompanyName = "Middle East Trading",
                    Country = "Egypt",
                    Notes = "International client",
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    IsDeleted = false
                }
            };

            await _context.Customers.AddRangeAsync(customers);
            await _context.SaveChangesAsync();
        }

        private async Task SeedServicesOnMainAsync()
        {
            if (await _context.ServicesOnMains.AnyAsync())
                return;

            var services = new List<ServicesOnMain>
            {
                new ServicesOnMain
                {
                    Id = 1,
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
                    Id = 2,
                    Title = "E-Commerce Solutions",
                    Description = "Complete e-commerce platform development",
                    Price = 8000.00m,
                    ImageUrl = "/images/services/ecommerce.jpg",
                    Category = "E-Commerce",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-12),
                    IsDeleted = false
                },
                new ServicesOnMain
                {
                    Id = 3,
                    Title = "Mobile App Development",
                    Description = "Native and cross-platform mobile applications",
                    Price = 10000.00m,
                    ImageUrl = "/images/services/mobile-app.jpg",
                    Category = "Mobile",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    IsDeleted = false
                },
                new ServicesOnMain
                {
                    Id = 4,
                    Title = "SEO Optimization",
                    Description = "Search engine optimization services",
                    Price = 2000.00m,
                    ImageUrl = "/images/services/seo.jpg",
                    Category = "Marketing",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-8),
                    IsDeleted = false
                }
            };

            await _context.ServicesOnMains.AddRangeAsync(services);
            await _context.SaveChangesAsync();
        }

        private async Task SeedServicesOnDashboardAsync()
        {
            if (await _context.ServicesOnDashBoards.AnyAsync())
                return;

            var services = new List<ServicesOnDashBoard>
            {
                new ServicesOnDashBoard
                {
                    Id = 1,
                    CustomerId = 1,
                    ServiceName = "John's WordPress Site",
                    Domain = "johndoe.com",
                    UserName = "johndoe_admin",
                    CMS = WebsiteCMS.WordPress,
                    HostingServer = "Bluehost",
                    Status = WebsiteStatus.Active,
                    Notes = "Production site - High traffic",
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    IsDeleted = false
                },
                new ServicesOnDashBoard
                {
                    Id = 2,
                    CustomerId = 2,
                    ServiceName = "Jane's E-Commerce Platform",
                    Domain = "janeshop.com",
                    UserName = "jane_admin",
                    CMS = WebsiteCMS.DotNet,
                    HostingServer = "Azure",
                    Status = WebsiteStatus.Active,
                    Notes = "E-commerce platform with payment integration",
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    IsDeleted = false
                },
                new ServicesOnDashBoard
                {
                    Id = 3,
                    CustomerId = 3,
                    ServiceName = "Ahmed's Business Site",
                    Domain = "ahmedbusiness.com",
                    UserName = "ahmed_user",
                    CMS = WebsiteCMS.Custom,
                    HostingServer = "AWS",
                    Status = WebsiteStatus.InProgress,
                    Notes = "Under development",
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    IsDeleted = false
                }
            };

            await _context.ServicesOnDashBoards.AddRangeAsync(services);
            await _context.SaveChangesAsync();
        }

        private async Task SeedEmailCredentialsAsync()
        {
            if (await _context.EmailCredentials.AnyAsync())
                return;

            var credentials = new List<EmailCredential>
            {
                new EmailCredential
                {
                    Id = 1,
                    CustomerId = 1,
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
                    Id = 2,
                    CustomerId = 2,
                    EmailAddress = "support@janeshop.com",
                    EncryptedPassword = _encryptionService.Encrypt("ShopPass456!"),
                    UserName = "jane_support",
                    HostingPlatform = "Azure",
                    Notes = "Support email for e-commerce platform",
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    IsDeleted = false
                },
                new EmailCredential
                {
                    Id = 3,
                    CustomerId = 3,
                    EmailAddress = "info@ahmedbusiness.com",
                    EncryptedPassword = _encryptionService.Encrypt("Business789!"),
                    UserName = "ahmed_info",
                    HostingPlatform = "AWS",
                    Notes = "Business contact email",
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    IsDeleted = false
                }
            };

            await _context.EmailCredentials.AddRangeAsync(credentials);
            await _context.SaveChangesAsync();
        }

        private async Task SeedExpensesAsync()
        {
            if (await _context.Expenses.AnyAsync())
                return;

            var expenses = new List<Expense>
            {
                new Expense
                {
                    Id = 1,
                    Title = "Server Hosting",
                    Amount = 150.00m,
                    Category = "Infrastructure",
                    Date = DateTime.UtcNow.AddDays(-5),
                    Notes = "Monthly hosting fee",
                    IsDeleted = false
                },
                new Expense
                {
                    Id = 2,
                    Title = "Domain Renewal",
                    Amount = 15.00m,
                    Category = "Domain",
                    Date = DateTime.UtcNow.AddDays(-3),
                    Notes = "Annual domain renewal",
                    IsDeleted = false
                },
                new Expense
                {
                    Id = 3,
                    Title = "SSL Certificate",
                    Amount = 50.00m,
                    Category = "Security",
                    Date = DateTime.UtcNow.AddDays(-1),
                    Notes = "SSL certificate purchase",
                    IsDeleted = false
                }
            };

            await _context.Expenses.AddRangeAsync(expenses);
            await _context.SaveChangesAsync();
        }

        private async Task SeedRevenuesAsync()
        {
            if (await _context.Revenues.AnyAsync())
                return;

            var revenues = new List<Revenue>
            {
                new Revenue
                {
                    Id = 1,
                    CustomerId = 1,
                    ServiceId = 1, // Points to ServicesOnDashBoard
                    Amount = 5000.00m,
                    PaymentDate = DateTime.UtcNow.AddDays(-20),
                    PaymentMethod = "Bank Transfer",
                    Notes = "Initial payment for website development",
                    IsDeleted = false
                },
                new Revenue
                {
                    Id = 2,
                    CustomerId = 2,
                    ServiceId = 2, // Points to ServicesOnDashBoard
                    Amount = 8000.00m,
                    PaymentDate = DateTime.UtcNow.AddDays(-15),
                    PaymentMethod = "Credit Card",
                    Notes = "E-commerce platform payment",
                    IsDeleted = false
                },
                new Revenue
                {
                    Id = 3,
                    CustomerId = 1,
                    ServiceId = 1, // Points to ServicesOnDashBoard
                    Amount = 2000.00m,
                    PaymentDate = DateTime.UtcNow.AddDays(-8),
                    PaymentMethod = "PayPal",
                    Notes = "Additional service payment",
                    IsDeleted = false
                }
            };

            await _context.Revenues.AddRangeAsync(revenues);
            await _context.SaveChangesAsync();
        }

        private async Task SeedContactMessagesAsync()
        {
            if (await _context.ContactMessages.AnyAsync())
                return;

            var messages = new List<ContactMessage>
            {
                new ContactMessage
                {
                    Id = 1,
                    FullName = "Sarah Johnson",
                    Email = "sarah.johnson@example.com",
                    Subject = "Website Development Inquiry",
                    Message = "I'm interested in your website development services. Please contact me.",
                    CreatedAt = DateTime.UtcNow.AddDays(-7),
                    IsRead = false
                },
                new ContactMessage
                {
                    Id = 2,
                    FullName = "Michael Brown",
                    Email = "michael.brown@example.com",
                    Subject = "Mobile App Development",
                    Message = "Looking for mobile app development. What are your rates?",
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    IsRead = true
                },
                new ContactMessage
                {
                    Id = 3,
                    FullName = "Emily Davis",
                    Email = "emily.davis@example.com",
                    Subject = "SEO Services",
                    Message = "Need help with SEO for my website. Can you provide a quote?",
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    IsRead = false
                }
            };

            await _context.ContactMessages.AddRangeAsync(messages);
            await _context.SaveChangesAsync();
        }
    }
}

