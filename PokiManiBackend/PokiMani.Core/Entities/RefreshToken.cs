using PokiMani.Core.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokiMani.Core.Entities
{
    public class RefreshToken : IUserOwned
    {
        public Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string Token { get; set; }
        public required DateTime Expires { get; set; }
        public required bool IsRevoked { get; set; } 
    }
}
