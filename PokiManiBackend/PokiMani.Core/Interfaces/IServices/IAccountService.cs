using PokiMani.Core.Entities;
using PokiMani.Core.DTOs;

namespace PokiMani.Core.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetUserAccountsAsync();

        Task<Account> GetAccountByIdAsync(Guid id);

        Task<Account> CreateAccountAsync(AccountDto dto);

        Task<Account> UpdateAccountAsync(Guid id, AccountDto dto);

        Task DeleteAccountAsync(Guid id);

    }
}
