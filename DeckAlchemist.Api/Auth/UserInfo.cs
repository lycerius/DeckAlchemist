using System;
using System.Security.Claims;

namespace DeckAlchemist.Api.Auth
{
    public static class UserInfo
    {
        public static string Id(ClaimsPrincipal user) => user.FindFirst("sub")?.Value;
        public static string Email(ClaimsPrincipal user) => user.FindFirst("email")?.Value;
        
    }
}
