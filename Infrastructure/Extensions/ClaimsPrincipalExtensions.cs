using System.Security.Claims;

namespace DemoAppBE.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal? principal)
        {
            string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(userId, out int parsedUserId) ?
                parsedUserId :
                throw new ApplicationException("User id is unavailable");
        }
    }
}
