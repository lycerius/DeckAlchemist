using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace DeckAlchemist.Api.Utility
{
    public static class UserInfo
    {
        public static string Id(this ClaimsPrincipal user) => user.FindFirst("user_id")?.Value;

        public static string Email(this ClaimsPrincipal user)
        {
            var firebase = user.FindFirst("firebase")?.Value;
            if (firebase == null) return null;
            dynamic properties = JObject.Parse(firebase);
            if (properties == null) return null;
            var email = properties.identities.email as JArray;
            return email.First.Value<string>();
      
        }

        public static bool AuthenticUser(this ClaimsPrincipal requestingUser, string userId)
        {
            return Id(requestingUser) == userId;
        }

        public static string GetIdToken(this HttpContext context)
        {
            var result = context.Request.Headers["Authorization"];
            return result;
        }
        
    }
}
