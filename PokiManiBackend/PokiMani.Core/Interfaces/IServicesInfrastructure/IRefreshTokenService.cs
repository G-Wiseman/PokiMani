using PokiMani.Core.Common;
using PokiMani.Core.Entities;


namespace PokiMani.Core.Interfaces.IServicesInfrastructure
{
    public interface IRefreshTokenService
    {
        public Task<Result<RefreshToken>> GenerateRefreshTokenAsync(CoreUser user);

        public Task<Result<RefreshToken>> ValidateAndCycleRefreshTokenAsync(string token);

        public Task RevokeTokenAsync(string token);

    }
}
