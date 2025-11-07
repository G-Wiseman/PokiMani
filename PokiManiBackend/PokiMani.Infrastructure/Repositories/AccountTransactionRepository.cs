using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IRepositories;
using PokiMani.Infrastructure.Data;

namespace PokiMani.Infrastructure.Repositories
{
    public class AccountTransactionRepository : UserOwnedBaseRepository<AccountTransaction, Guid>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(ApplicationDbContext context) : base(context) { }
    }
}
