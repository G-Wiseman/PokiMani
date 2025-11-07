using PokiMani.Core.Entities;
using PokiMani.Core.DTOs;

namespace PokiMani.Core.Interfaces.IServices
{
    public interface IAccountTransactionService
    {
        Task<IEnumerable<AccountTransaction>> GetUserAccountTransactionsAsync();

        Task<AccountTransaction> GetAccountTransactionByIdAsync(Guid id);

        Task<AccountTransaction> CreateAccountTransactionAsync(CreateAccountTransactionDto dto);

        Task<AccountTransaction> UpdateAccountTransactionAsync(Guid id, AccountTransactionDto dto);

        Task DeleteAccountTransactionAsync(Guid id);

    }
}
