using Microsoft.AspNetCore.Identity;

namespace PokiMani.Core.Entities
{
    public class CoreUser
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }
    }
}
