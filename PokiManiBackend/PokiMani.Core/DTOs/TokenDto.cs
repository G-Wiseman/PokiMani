using Microsoft.AspNetCore.Authentication.BearerToken;

namespace PokiMani.Core.DTOs
{
    public class TokenDto
    {
        public required string AccessToken { get; set; }
    }
}
