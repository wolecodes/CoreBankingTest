using CoreBanking.Test.Core.Entities;

namespace CoreBanking.Test.Core.Interfaces
{
  
  public interface ICustomerRespository
  {
    // Define methods for customer repository

    Task<Customer> GetByIdAsync(Guid customerId);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task<bool> ExistsAsync(Guid customerId);
  }
}