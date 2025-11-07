using PokiMani.Core.Entities;

namespace PokiMani.Core.Interfaces.IRepositories
{
    public interface IAccountRepository: IUserOwnedBaseRepository<Account, Guid>
    {
    }
}
