using CoreBanking.Test.Core.Models;

namespace CoreBanking.Test.Core.Interfaces
{
    public interface IAccountRepository
    {
    AccountModel? GetAccountById(int id);
    IEnumerable<AccountModel> GetAllAccounts();
    void Add(AccountModel account);
    }
}