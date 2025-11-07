using PokiMani.Core.Entities;

namespace PokiMani.Core.Interfaces.IRepositories
{
    public interface IEnvelopeTransactionRepository : IUserOwnedBaseRepository<EnvelopeTransaction, Guid>
    {
    }
}
