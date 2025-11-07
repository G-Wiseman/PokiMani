using PokiMani.Core.Entities;
using PokiMani.Core.DTOs;

namespace PokiMani.Core.Interfaces.IServices
{
    public interface IEnvelopeService
    {
        Task<IEnumerable<Envelope>> GetUserEnvelopesAsync();

        Task<Envelope> GetEnvelopeByIdAsync(Guid id);

        Task<Envelope> CreateEnvelopeAsync(EnvelopeDto dto);

        Task<Envelope> UpdateEnvelopeAsync(Guid id, EnvelopeDto dto);

        Task DeleteEnvelopeAsync(Guid id);

    }
}
