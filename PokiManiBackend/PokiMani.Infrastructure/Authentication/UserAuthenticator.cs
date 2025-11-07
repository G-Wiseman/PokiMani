using Microsoft.AspNetCore.Identity;
using PokiMani.Core.Common;
using PokiMani.Core.Entities;
using PokiMani.Core.Interfaces.IServicesInfrastructure;
using PokiMani.Core.Services;

namespace PokiMani.Infrastructure.Authentication
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public UserAuthenticator(
            UserManager<IdentityUser<Guid>> userManager,
            SignInManager<IdentityUser<Guid>> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<CoreUser>> AuthenticateAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Result<CoreUser>.Failure("Invalid Username or Password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
            { 
                return Result<CoreUser>.Failure("Invalid Username or Password"); ;
            }

            return Result<CoreUser>.Success( new CoreUser
            {
                Id = user.Id,
                Name = user.UserName!,
                Email = user.Email ?? string.Empty
            });
        }

        public async Task<Result<CoreUser>> CreateNewUserAync(string username, string email, string password)
        {
            Console.WriteLine($"Password received: '{password}'");
            var newUserInput = new IdentityUser<Guid>
            {
                UserName = username,
                Email = email,
            };
            var userResult = await _userManager.CreateAsync(newUserInput, password);
            if (!userResult.Succeeded) 
            {
                
                var errors = string.Join("; ", userResult.Errors.Select(e => e.Description));
                return Result<CoreUser>.Failure(string.IsNullOrWhiteSpace(errors) ? "UnknownError" : errors);
            }
            return Result<CoreUser>.Success(new CoreUser
            {
                Id = newUserInput.Id,
                Name = username,
                Email = email ?? string.Empty
            });
        }

        public async Task<Result<CoreUser>> GetUserByIdAsync(Guid id)
        {
            var identityUser = await _userManager.FindByIdAsync(id.ToString());

            if (identityUser == null)
            {
                return Result<CoreUser>.Failure("User not found");
            }

            return Result<CoreUser>.Success(new CoreUser
            {
                Id = identityUser.Id,
                Name = identityUser.UserName ?? string.Empty,
                Email = identityUser.Email ?? string.Empty
            });
        }

        public async Task<Result<CoreUser>> GetUserByNameAsync(string username)
        {
            var identityUser = await _userManager.FindByNameAsync(username);

            if (identityUser == null)
            {
                return Result<CoreUser>.Failure("User not found");
            }

            return Result<CoreUser>.Success(new CoreUser
            {
                Id = identityUser.Id,
                Name = identityUser.UserName ?? string.Empty,
                Email = identityUser.Email ?? string.Empty
            });
        }
    }
}
