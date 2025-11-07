using PokiMani.Core.Interfaces.IServicesInfrastructure;
using System.Security.Claims;

namespace PokiMani.Infrastructure.Authentication
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?
                     .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                    throw new UnauthorizedAccessException("User ID claim is invalid.");

                if (!Guid.TryParse(userIdClaim, out var userId))
                    throw new UnauthorizedAccessException("User ID claim is invalid.");

                return userId;
            }
        }
    }
}