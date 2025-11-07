using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IRepositories;
using PokiMani.Infrastructure.Data;

namespace PokiMani.Infrastructure.Repositories
{
    public class EnvelopeTransactionRepository
        : UserOwnedBaseRepository<EnvelopeTransaction, Guid>, IEnvelopeTransactionRepository
    {
        public EnvelopeTransactionRepository(ApplicationDbContext context) : base(context) { }
    }
}
