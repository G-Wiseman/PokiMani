using PokiMani.Core.Entities;

namespace PokiMani.Core.Interfaces.IRepositories
{
    public interface IAccountTransactionRepository : IUserOwnedBaseRepository<AccountTransaction, Guid>
    {
    }
}
