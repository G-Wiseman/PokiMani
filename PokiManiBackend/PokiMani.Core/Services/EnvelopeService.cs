using PokiMani.Core.DTOs;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IRepositories;
using PokiMani.Core.Interfaces.IServices;
using PokiMani.Core.Interfaces.IServicesInfrastructure;

namespace PokiMani.Core.Services
{
    public class EnvelopeService : IEnvelopeService
    {
        IUserContext _userContext;
        IEnvelopeRepository _envelopeRepository;
        public EnvelopeService(IUserContext userContext, IEnvelopeRepository envelopeRepository) 
        {
            _userContext = userContext;
            _envelopeRepository = envelopeRepository;
        }

        public async Task<Envelope> CreateEnvelopeAsync(EnvelopeDto dto)
        {

            Envelope env = new Envelope
            {
                UserId = _userContext.UserId,
                isParent = dto.IsParent ?? false,
                name = dto.name,
                color = dto.Color,
                dateCreated = dto.DateCreated ?? DateTime.UtcNow,
                dateDestroyed = dto.DateDestroyed,
                balance = dto.Balance ?? 0
            };

            await _envelopeRepository.AddAsync(env, _userContext.UserId);
            return env;
        }

        public async Task DeleteEnvelopeAsync(Guid id)
        {
           
            // TODO: This will affect much more than the envelopes alone. This will affect envelope transactions as well. 
            await _envelopeRepository.DeleteAsync(id, _userContext.UserId); 
            
        }

        public async Task<Envelope> GetEnvelopeByIdAsync(Guid id)
        {
            
            var result = await _envelopeRepository.GetByIdAsync(id, _userContext.UserId);
            if (result != null) { return result; }

            throw new Exception("Either don't have access or doesn't exist");
        }

        public async Task<IEnumerable<Envelope>> GetUserEnvelopesAsync()
        {
            return await _envelopeRepository.GetAllAsync(_userContext.UserId);
        }

        public Task<Envelope> UpdateEnvelopeAsync(Guid id, EnvelopeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
