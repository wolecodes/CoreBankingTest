using CoreBanking.Test.Core.Entities;
using CoreBanking.Test.Core.ValueObjects;
namespace CoreBanking.Test.Core.Interfaces
{
  public interface ICustomerRepository
  {
    // Define methods for customer repository

    Task<Customer> GetByIdAsync(CustomerId customerId);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task<bool> ExistsAsync(CustomerId customerId);
  }
}