using PokiMani.Core.Entities;
using PokiMani.Core.Common;
namespace PokiMani.Core.Interfaces.IServices
{
    public interface IAuthManagerService
    {
        public Task<Result<(string Jwt, string RefreshToken, CoreUser user)>> LoginWithPasswordAsync(string username, string password);
        public Task<Result<(string Jwt, string RefreshToken, CoreUser user)>> LoginWithRefreshTokenAsync(string? refreshToken);

        public Task<Result<(string Jwt, string RefreshToken, CoreUser user)>> RegisterNewUser(string username, string password, string email);
    }
}
