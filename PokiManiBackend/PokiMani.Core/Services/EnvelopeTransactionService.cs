using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IRepositories;
using PokiMani.Core.Interfaces.IServices;
using PokiMani.Core.Interfaces.IServicesInfrastructure;

namespace PokiMani.Core.Services
{
    public class EnvelopeTransactionService : IEnvelopeTransactionService
    {
        IUserContext _userContext;
        IEnvelopeRepository _envelopeRepository;
        IEnvelopeTransactionRepository _envelopeTransactionRepository;
        public EnvelopeTransactionService(IUserContext userContext, IEnvelopeRepository envelopeRepository, IEnvelopeTransactionRepository envelopeTransactionRepository)
        {
            _userContext = userContext;
            _envelopeRepository = envelopeRepository;
            _envelopeTransactionRepository = envelopeTransactionRepository;
        }

        public async Task<EnvelopeTransaction> CreateEnvelopeTransactionAsync(CreateEnvelopeTransactionDto dto)
        {
            EnvelopeTransaction envT = new EnvelopeTransaction
            {
                UserId = _userContext.UserId,
                EnvelopeId = dto.EnvelopeId,
                AccountTransactionId = dto.AccountTransactionId,
                amount = dto.amount
            };
            await _envelopeTransactionRepository.AddAsync(envT, _userContext.UserId);
            if (envT.EnvelopeId != null)
            {
                var envelope = await _envelopeRepository.GetByIdAsync(envT.EnvelopeId.Value, _userContext.UserId);
                if (envelope == null) { throw new Exception("That's not a real envelope"); }
                envelope.balance += envT.amount;
                await _envelopeRepository.UpdateAsync(envelope, _userContext.UserId);
            }
            return envT;
        }

        public async Task DeleteEnvelopeTransactionAsync(Guid id)
        {
            var envelopeTransaction = await _envelopeTransactionRepository.GetByIdAsync(id, _userContext.UserId);
            if (envelopeTransaction == null) { return; }

            if (envelopeTransaction.EnvelopeId != null)
            {
                var envelope = await _envelopeRepository.GetByIdAsync(envelopeTransaction.EnvelopeId.Value, _userContext.UserId);
                if (envelope == null) { throw new Exception(); }
                envelope.balance -= envelopeTransaction.amount;
                await _envelopeRepository.UpdateAsync(envelope, _userContext.UserId);
            }
            await _envelopeTransactionRepository.DeleteAsync(id, _userContext.UserId);
        }

        public async Task<EnvelopeTransaction> GetEnvelopeTransactionByIdAsync(Guid id)
        {
            var result = await _envelopeTransactionRepository.GetByIdAsync(id, _userContext.UserId);
            if (result == null) { throw new Exception(); }  
            return result;
        }

        public async Task<IEnumerable<EnvelopeTransaction>> GetUserEnvelopeTransactionsAsync()
        {
            var result = await _envelopeTransactionRepository.GetAllAsync(_userContext.UserId);
            return result;
        }

        public Task<EnvelopeTransaction> UpdateEnvelopeTransactionsAsync(Guid id, EnvelopeTransactionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
