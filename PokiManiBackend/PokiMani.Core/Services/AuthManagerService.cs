using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IServices;
using PokiMani.Core.Interfaces.IServicesInfrastructure;
using PokiMani.Core.Services;
using PokiMani.Core.Common;

namespace PokiMani.Core.Services
{
    public class AuthManagerService : IAuthManagerService
    {
        private readonly IJwtService _tokenProvider;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUserAuthenticator _userAuthenticator;

        public AuthManagerService(

            IJwtService tokenProvider,
            IRefreshTokenService refreshTokenService,
            IUserAuthenticator userAuthenticator)
        {
            _tokenProvider = tokenProvider;
            _refreshTokenService = refreshTokenService;
            _userAuthenticator = userAuthenticator;
        }

        public async Task<Result<(string Jwt, string RefreshToken, CoreUser user)>> LoginWithPasswordAsync(string username, string password)
        {
            var userResult = await _userAuthenticator.AuthenticateAsync(username, password);
            if (!userResult.Succeeded) { return Result<(string, string, CoreUser)>.Failure(userResult.Error!); }

            var jwtResult = await _tokenProvider.GenerateJwt(userResult.Value!);
            if (!jwtResult.Succeeded) { return Result<(string, string, CoreUser)>.Failure(jwtResult.Error!); }

            var refreshTokenResult = await _refreshTokenService.GenerateRefreshTokenAsync(userResult.Value!);
            if (!refreshTokenResult.Succeeded) { return Result<(string, string, CoreUser)>.Failure(refreshTokenResult.Error!); }

            return Result<(string, string, CoreUser)>.Success((jwtResult.Value!, refreshTokenResult.Value!.Token, userResult.Value!));
        }

        public async Task<Result<(string Jwt, string RefreshToken, CoreUser user)>> LoginWithRefreshTokenAsync(string? refreshToken)
        {
            if (refreshToken == null) { return Result<(string, string, CoreUser)>.Failure("Passed a null refresh token"); }

            var refreshTokenResult = await _refreshTokenService.ValidateAndCycleRefreshTokenAsync(refreshToken);
            if (!refreshTokenResult.Succeeded) { return Result<(string, string, CoreUser)>.Failure(refreshTokenResult.Error!); }

            var userResult = await _userAuthenticator.GetUserByIdAsync(refreshTokenResult.Value!.UserId);
            if (!userResult.Succeeded) { return Result<(string, string, CoreUser)>.Failure(userResult.Error!); }

            var jwtResult = await _tokenProvider.GenerateJwt(userResult.Value!);
            if (!jwtResult.Succeeded) { return Result<(string, string, CoreUser)>.Failure(jwtResult.Error!); }

            return Result<(string, string, CoreUser)>.Success((jwtResult.Value!, refreshTokenResult.Value!.Token, userResult.Value!));
        }

        public async Task LogoutRefreshSessionAsync(string? refreshToken)
        {
            if (refreshToken != null)
            {
                await _refreshTokenService.RevokeTokenAsync(refreshToken);
            }
        }

        public async Task<Result<(string Jwt, string RefreshToken, CoreUser user)>> RegisterNewUserAsync(string username, string password, string email)
        {
            var userResult = await _userAuthenticator.CreateNewUserAync(username: username, email: email, password:password);
            if (!userResult.Succeeded) { return Result<(string, string, CoreUser)>.Failure(userResult.Error!); }

            var jwtResult = await _tokenProvider.GenerateJwt(userResult.Value!);
            if (!jwtResult.Succeeded) { return Result<(string, string, CoreUser)>.Failure(jwtResult.Error!); }

            var refreshTokenResult = await _refreshTokenService.GenerateRefreshTokenAsync(userResult.Value!);
            if (!refreshTokenResult.Succeeded) { return Result<(string, string, CoreUser)>.Failure(refreshTokenResult.Error!); }

            return Result<(string, string, CoreUser)>.Success((jwtResult.Value!, refreshTokenResult.Value!.Token, userResult.Value!));
        }
    }
}
