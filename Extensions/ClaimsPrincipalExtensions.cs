using System.Security.Claims;

namespace Talkify.Extensions
{
    public static class ClaimsPrincipalExtensions
    {

        public static string GetUserId(this ClaimsPrincipal user)
        {
            var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id is null) return null!;
            return id;
        }

        public static string GetUsername(this ClaimsPrincipal user)
        {
            var name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (name is null) return null!;
            return name;
        }

    }
}