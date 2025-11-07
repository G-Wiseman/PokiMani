using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PokiMani.Core.Interfaces.IRepositories;
using PokiMani.Infrastructure.Data;

namespace PokiMani.Infrastructure.Repositories
{
    public class UserOwnedBaseRepository<T, Tid> : IUserOwnedBaseRepository<T, Tid> where T : class, IUserOwned
    {

        ApplicationDbContext _dbContext;
        public UserOwnedBaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity, Guid userId)
        {
            if (entity.UserId != userId) { return; }
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tid id, Guid userId)
        {
            T? data = await _dbContext.Set<T>().FindAsync(id);
            if (data == null) { return; }
            if (data.UserId != userId) { return; }

            _dbContext.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(Tid id, Guid userId)
        {
            var data = await _dbContext.Set<T>().FindAsync(id);
            if (data == null) { return null; }
            if (data.UserId != userId) { return null; }
            
            return data;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Guid userId)
        {
            var data = await _dbContext.Set<T>()
                .Where(e => e.UserId == userId)
                .ToListAsync();

            return data;
        }

        public async Task UpdateAsync(T entity, Guid userId)
        {
            var data = await _dbContext.Set<T>().FindAsync(entity.Id);
            if (data == null) { return; }
            if (data.UserId != userId) { return; }

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

