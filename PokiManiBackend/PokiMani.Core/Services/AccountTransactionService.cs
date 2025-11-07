using Microsoft.JSInterop.Infrastructure;
using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IRepositories;
using PokiMani.Core.Interfaces.IServices;
using PokiMani.Core.Interfaces.IServicesInfrastructure;

namespace PokiMani.Core.Services
{
    public class AccountTransactionService : IAccountTransactionService
    {
        IUserContext _userContext;
        IAccountTransactionRepository _accountTransactionRepository;
        IAccountRepository _accountRepository;

        public AccountTransactionService(IUserContext userContext, IAccountTransactionRepository AccountTransactionRepository, IAccountRepository accountRepository)
        {
            _userContext = userContext;
            _accountTransactionRepository = AccountTransactionRepository;
            _accountRepository = accountRepository;
        }
        public async Task<AccountTransaction> CreateAccountTransactionAsync(CreateAccountTransactionDto dto)
        {
            Account? acc = await _accountRepository.GetByIdAsync(dto.AccountId, _userContext.UserId);
            if (acc == null) { throw new Exception("Account doesn't exist or doesn't belong to you"); }           
            
            AccountTransaction transaction = new AccountTransaction
            {
                UserId = _userContext.UserId,
                AccountId = dto.AccountId,
                amount = dto.amount,
                cleared = false,
                reconciled = false, 
                memo = dto.memo, 
                payee = dto.payee,
                notes = dto.notes,
                
            };

            acc.balance += transaction.amount;

            await _accountTransactionRepository.AddAsync(transaction, _userContext.UserId);
            await _accountRepository.UpdateAsync(acc, _userContext.UserId);
            return transaction;
        }

        public async Task DeleteAccountTransactionAsync(Guid id)
        {  
            var transaction = await _accountTransactionRepository.GetByIdAsync(id, _userContext.UserId);
            if (transaction == null) { return; }
            var acc = await _accountRepository.GetByIdAsync(transaction!.AccountId, _userContext.UserId);
            acc!.balance -= transaction.amount;
            await _accountRepository.UpdateAsync(acc, _userContext.UserId);

            await _accountTransactionRepository.DeleteAsync(id, _userContext.UserId);
        }

        public async Task<AccountTransaction> GetAccountTransactionByIdAsync(Guid id)
        {
            AccountTransaction? acc = await _accountTransactionRepository.GetByIdAsync(id, _userContext.UserId);
            if (acc == null) { throw new Exception("this probably isn't the best way to handle this"); }
            return acc;
        }

        public async Task<IEnumerable<AccountTransaction>> GetUserAccountTransactionsAsync()
        {
            IEnumerable<AccountTransaction>? acc = await _accountTransactionRepository.GetAllAsync(_userContext.UserId);
            if (acc == null) { throw new Exception("this probably isn't the best way to handle this"); }
            return acc;
        }

        public Task<AccountTransaction> UpdateAccountTransactionAsync(Guid id, AccountTransactionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
