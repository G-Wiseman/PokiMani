using PokiMani.Core.Entities;
using System.Threading.Tasks;
using PokiMani.Core.Common;

namespace PokiMani.Core.Interfaces.IServicesInfrastructure
{
    public interface IUserAuthenticator
    {
        Task<Result<CoreUser>> CreateNewUserAync(string username, string email, string password);
        Task<Result<CoreUser>> AuthenticateAsync(string username, string password);
        Task<Result<CoreUser>> GetUserByNameAsync(string username);
        Task<Result<CoreUser>> GetUserByIdAsync(Guid id);
    }
}
