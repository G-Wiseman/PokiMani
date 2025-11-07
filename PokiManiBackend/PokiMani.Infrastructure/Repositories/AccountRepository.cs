using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IRepositories;
using PokiMani.Infrastructure.Data;

namespace PokiMani.Infrastructure.Repositories
{
    public class AccountRepository : UserOwnedBaseRepository<Account, Guid>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context) { }
    }
}
