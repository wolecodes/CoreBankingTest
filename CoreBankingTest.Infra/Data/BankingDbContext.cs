using Microsoft.EntityFrameworkCore;
using CoreBanking.Test.Core.Entities;
using CoreBanking.Test.Core.ValueObjects;
using CoreBanking.Test.Core.Enums;
namespace CoreBankingTest.Infra.Data
{
  public class BankingDbContext : DbContext
  {

    public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure entity properties and relationships here if needed

      modelBuilder.Entity<Customer>(entity =>
      {
        entity.HasKey(e => e.CustomerId);
        entity.Property(c => c.CustomerId).HasConversion(customerId => customerId.Value, value => new CustomerId(value));
        entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
        entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);

        entity.HasMany(c => c.Accounts).WithOne(a => a.Customer).HasForeignKey(a => a.CustomerId);
       });



      // Account entity configuration
      modelBuilder.Entity<Account>(entity =>
      {
        entity.HasKey(e => e.AccountId);
        entity.Property(e => e.AccountNumber).HasColumnName("AccountNumber").IsRequired().HasMaxLength(10);
        entity.OwnsOne(e => e.Balance, money =>
        {
          money.Property(m => m.Amount).HasColumnName("Balance Amount").HasPrecision(18, 2);
          money.Property(m => m.Currency).HasColumnName("Balance Currency").HasMaxLength
          (3).HasDefaultValue("NGN");
        });

        entity.Property(a => a.AccountType).HasConversion<string>().IsRequired();

        entity.Property(a => a.RowVersion)
          .IsRowVersion()
          .IsConcurrencyToken();

        // Account has many transactions
        entity.HasMany(a => a.Transactions)
              .WithOne(t => t.Account)
              .HasForeignKey(t => t.AccountId);

        // Ensure we don't accidentally load all transactions

        entity.Navigation(a => a.Transactions).AutoInclude(false);
      });



      modelBuilder.Entity<Transaction>(entity =>
      {
        entity.HasKey(e => e.TransactionId);
        entity.OwnsOne(t => t.Amount, money =>
        {
          money.Property(m => m.Amount).HasColumnName("Amount").HasPrecision(18, 2);
          money.Property(m => m.Currency).HasColumnName("Currency").HasMaxLength(3);
        });

        entity.Property(t => t.Type).HasConversion<string>().IsRequired();

        entity.Property(t => t.Description).HasMaxLength(500);
        entity.Property(t => t.Reference).HasMaxLength(50);
        entity.Property(t => t.Timestamp).IsRequired();

      });
      // modelBuilder.Entity<Customer>().HasData(
      //     new Customer(
      //         customerId: Guid.Parse("a1b2c3d4-1234-5678-9abc-123456789abc"),
      //         firstName: "Alice", 
      //         lastName: "Johnson",
      //         email: "alice.johnson@email.com", 
      //         phoneNumber: "555-0101"
      //     ) {
      //         DateCreated = DateTime.UtcNow.AddDays(-30)
      //     }
      // );

      // modelBuilder.Entity<Account>().HasData(
      //     new Account(
      //         accountNumber: new AccountNumber("1000000001"),
      //         accountType: AccountType.Checking,
      //         customerId: Guid.Parse("a1b2c3d4-1234-5678-9abc-123456789abc")
      //     ) {
      //         AccountId = Guid.Parse("c3d4e5f6-3456-7890-cde1-345678901cde"),
      //         Balance = new Money(1500.00m)
      //     }


      modelBuilder.Entity<Customer>().HasData(new {
                    CustomerId = CustomerId.Create(Guid.Parse("a1b2c3d4-1234-5678-9abc-123456789abc")),
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@email.com",
                    PhoneNumber = "555-0101",
                    DateCreated = DateTime.UtcNow.AddDays(-30),
                    IsActive = true,
                    IsDeleted = false
                }
            );

            modelBuilder.Entity<Account>().HasData(new {
                    AccountId = AccountId.Create(Guid.Parse("c3d4e5f6-3456-7890-cde1-345678901cde")),
                    AccountType = AccountType.Checking, // EF handles enum conversion
                    CustomerId = CustomerId.Create(Guid.Parse("a1b2c3d4-1234-5678-9abc-123456789abc")),
                    Currency = "NGN",
                    DateOpened = DateTime.UtcNow.AddDays(-20),
                    IsActive = true,
                    IsDeleted = false            
                }
            );

            // Then configure the owned types separately
            modelBuilder.Entity<Account>().OwnsOne(a => a.AccountNumber).HasData(
                new
                {
                    AccountId = AccountId.Create(Guid.Parse("c3d4e5f6-3456-7890-cde1-345678901cde")),
                    Value = "1000000001"
                }
            );

            modelBuilder.Entity<Account>().OwnsOne(a => a.Balance).HasData(
                new
                {
                    AccountId = AccountId.Create(Guid.Parse("c3d4e5f6-3456-7890-cde1-345678901cde")),
                    Amount = 1500.00m,
                    Currency = "NGN"
                }
            );

    }
  }

}