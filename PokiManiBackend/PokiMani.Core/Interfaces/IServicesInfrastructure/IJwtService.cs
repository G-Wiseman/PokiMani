using Microsoft.AspNetCore.Identity;
using PokiMani.Core.Common;
using PokiMani.Core.Entities;


namespace PokiMani.Core.Interfaces.IServicesInfrastructure
{
    public interface IJwtService
    {
        Task<Result<string>> GenerateJwt(CoreUser user);
    }
}