using System.Security.Claims;
using Newtonsoft.Json.Linq;

namespace DeckAlchemist.Api.Auth
{
    public static class UserInfo
    {
        public static bool IsLoggedIn(this ClaimsPrincipal user) => Id(user) != null;
        public static string Id(ClaimsPrincipal user) => user.FindFirst("user_id")?.Value;
        public static string Email(ClaimsPrincipal user) {
            var firebase = user.FindFirst("firebase")?.Value;
            if (firebase == null) return null;
            dynamic properties = JObject.Parse(firebase);
            if (properties == null) return null;
            var email = properties.identities.email as JArray;
            return email.First.Value<string>();
        }
    }
}
