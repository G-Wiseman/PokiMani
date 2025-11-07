using Microsoft.AspNetCore.SignalR;

namespace PokiMani.Core.Interfaces.IRepositories
{
    public interface IUserOwned
    {
        Guid Id { get; }
        Guid UserId { get; }
    }
}
