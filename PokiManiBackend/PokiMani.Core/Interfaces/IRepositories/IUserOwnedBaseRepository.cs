
namespace PokiMani.Core.Interfaces.IRepositories
{
    public interface IUserOwnedBaseRepository<T, TId> where T : class, IUserOwned
    {
        // UserId to prevent from EVER giving out multiple user's info, as well as ensuring only correct user can access. 
        Task<T?> GetByIdAsync(TId id, Guid userId);
        Task<IReadOnlyList<T>> GetAllAsync(Guid userId);
        Task AddAsync(T entity, Guid userId); 
        Task UpdateAsync(T entity, Guid userId);
        Task DeleteAsync(TId id, Guid userId);
    }
}
