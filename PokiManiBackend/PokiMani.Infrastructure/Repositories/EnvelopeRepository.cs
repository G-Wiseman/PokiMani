using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IRepositories;
using PokiMani.Infrastructure.Data;

namespace PokiMani.Infrastructure.Repositories
{
    public class EnvelopeRepository : UserOwnedBaseRepository<Envelope, Guid>, IEnvelopeRepository
    {
        public EnvelopeRepository(ApplicationDbContext context) : base(context) { }
    }
}
