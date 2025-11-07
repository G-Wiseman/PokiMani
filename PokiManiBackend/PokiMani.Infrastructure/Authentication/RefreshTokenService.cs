using Microsoft.EntityFrameworkCore;
using PokiMani.Core.Common;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IServicesInfrastructure;
using PokiMani.Infrastructure.Data;

namespace PokiMani.Core.Services
{
    public sealed class RefreshTokenService: IRefreshTokenService
    {
        private readonly ApplicationDbContext _dbContext;

        public RefreshTokenService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<RefreshToken>> GenerateRefreshTokenAsync(CoreUser user)
        {
            var token = Guid.NewGuid().ToString();
            var refreshToken = new RefreshToken
            {
                Token = token,
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };
            _dbContext.RefreshTokens.Add(refreshToken);
            await _dbContext.SaveChangesAsync();
            return Result<RefreshToken>.Success(refreshToken);
        }

        public async Task<Result<RefreshToken>> ValidateAndCycleRefreshTokenAsync(string token)
        {
            
            var storedToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (storedToken == null || storedToken.IsRevoked || storedToken.Expires < DateTime.UtcNow)
                return Result<RefreshToken>.Failure("Token not Valid");

            await RevokeTokenAsync(token);

            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == storedToken.UserId);
            if (user  == null) { return Result<RefreshToken>.Failure("No User associated with String"); }



            return await GenerateRefreshTokenAsync(new CoreUser
            {
                Id = user.Id,
                Name = user.UserName!,
                Email = user.Email!,
            });
        }

        public async Task RevokeTokenAsync(string token)
        {
            var storedToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (storedToken == null) { return; }
            storedToken.IsRevoked = true;
            await _dbContext.SaveChangesAsync();
            return;
        }
    }
}
