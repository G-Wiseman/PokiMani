using PokiMani.Core.Entities;

namespace PokiMani.Core.DTOs
{
    public class SessionDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }

}