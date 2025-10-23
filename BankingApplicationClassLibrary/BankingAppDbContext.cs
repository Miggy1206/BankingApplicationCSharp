using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankingApplicationClassLibrary
{
    public class BankingAppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<CurrentAccount>().ToTable("CurrentAccounts");
            modelBuilder.Entity<SavingsAccount>().ToTable("SavingsAccounts");
            modelBuilder.Entity<MortgageAccount>().ToTable("Mortgages");
            modelBuilder.Entity<CreditAccount>().ToTable("CreditCards");


            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Staff>().ToTable("StaffMembers");
        }

    }
}

// ENC0023 is a Visual Studio Edit and Continue error that occurs when you add or override abstract/virtual methods during debugging.
// To resolve this, you must stop debugging and restart the application before making changes to OnModelCreating or OnConfiguring overrides.
// No code changes are required for the error itself, but you must restart your application after editing these methods.
