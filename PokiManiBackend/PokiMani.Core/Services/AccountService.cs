using Microsoft.JSInterop.Infrastructure;
using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IRepositories;
using PokiMani.Core.Interfaces.IServices;
using PokiMani.Core.Interfaces.IServicesInfrastructure;

namespace PokiMani.Core.Services
{
    public class AccountService : IAccountService
    {
        IUserContext _userContext;
        IAccountRepository _accountRepository;

        public AccountService(IUserContext userContext, IAccountRepository accountRepository)
        {
            _userContext = userContext;
            _accountRepository = accountRepository;
        }
        public async Task<Account> CreateAccountAsync(AccountDto dto)
        {
            Account acc = new Account
            {
                UserId = _userContext.UserId,
                name = dto.name ?? "New_Account",
                createdDate = dto.createdDate ?? DateTime.UtcNow,
                balance = dto.balance ?? 0,
                closedDate = dto.closedDate
            };
            await _accountRepository.AddAsync(acc, _userContext.UserId);
            return acc;
        }

        public async Task DeleteAccountAsync(Guid id)
        {  
            // Note: Do I want it to "mark it as closed" but still keep it OR delete all transaction.
            // On this more aggress DELETE i'm leaning towards the second option. But maybe making first option possible with a patch is a good idea.
            // Needs to be one of these things. Transaction should not exist in the system without an account
            await _accountRepository.DeleteAsync(id, _userContext.UserId);
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            Account? acc = await _accountRepository.GetByIdAsync(id, _userContext.UserId);
            if (acc == null) { throw new Exception("this probably isn't the best way to handle this"); }
            return acc;
        }

        public async Task<IEnumerable<Account>> GetUserAccountsAsync()
        {
            IEnumerable<Account>? acc = await _accountRepository.GetAllAsync(_userContext.UserId);
            if (acc == null) { throw new Exception("this probably isn't the best way to handle this"); }
            return acc;
        }

        public Task<Account> UpdateAccountAsync(Guid id, AccountDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
