using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;

namespace PokiMani.Core.Interfaces.IServices
{
    public interface IEnvelopeTransactionService
    {
        Task<IEnumerable<EnvelopeTransaction>> GetUserEnvelopeTransactionsAsync();

        Task<EnvelopeTransaction> GetEnvelopeTransactionByIdAsync(Guid id);

        Task<EnvelopeTransaction> CreateEnvelopeTransactionAsync(CreateEnvelopeTransactionDto dto);

        Task<EnvelopeTransaction> UpdateEnvelopeTransactionsAsync(Guid id, EnvelopeTransactionDto dto);
        Task DeleteEnvelopeTransactionAsync(Guid id);

    }
}
