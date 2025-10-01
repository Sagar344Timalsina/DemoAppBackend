using DemoAppBE.Infrastructure.Extensions;

namespace DemoAppBE.Shared
{
    public sealed class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Id =>
            _httpContextAccessor
                .HttpContext?
                .User
                .GetUserId() ??
            throw new ApplicationException("User context is unavailable");

    }
}
