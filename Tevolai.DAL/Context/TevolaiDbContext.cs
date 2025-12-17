using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Context
{
    public class TevolaiDbContext : IdentityDbContext<ApplicationUser>
    {
        public TevolaiDbContext(DbContextOptions<TevolaiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision to avoid truncation warnings
            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Revenue>()
                .Property(r => r.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ServicesOnMain>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);

            // Configure Revenue relationships to avoid cascade path conflicts
            // Revenue -> Customer: NO ACTION (since we use soft deletes)
            // Revenue -> ServicesOnDashBoard: NO ACTION (to avoid cascade path through Customer)
            modelBuilder.Entity<Revenue>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Revenues)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Revenue>()
                .HasOne(r => r.Service)
                .WithMany()
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            //global query filter to exclude soft-deleted entities
            modelBuilder.Entity<EmailCredential>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Customer>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<ServicesOnDashBoard>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<ServicesOnMain>().HasQueryFilter(s => !s.IsDeleted);
            //modelBuilder.Entity<ContactMessage>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Revenue>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Expense>().HasQueryFilter(e => !e.IsDeleted);
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ServicesOnDashBoard> ServicesOnDashBoards { get; set; }
        public virtual DbSet<ServicesOnMain> ServicesOnMains { get; set; }
        public virtual DbSet<ContactMessage> ContactMessages { get; set; }
        public virtual DbSet<Revenue> Revenues { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<EmailCredential> EmailCredentials { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}