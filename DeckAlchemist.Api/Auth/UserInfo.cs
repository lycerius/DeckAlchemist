using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeckAlchemist.Api.Auth
{
    public static class UserInfo
    {
        public static string Id(ClaimsPrincipal user) => user.FindFirst("user_id")?.Value;
        public static string Email(ClaimsPrincipal user) {
            var firebase = user.FindFirst("firebase")?.Value;
            if (firebase == null) return null;
            dynamic properties = JObject.Parse(firebase);
            if (properties == null) return null;
            var email = properties.identities.email as JArray;
            return email.First.Value<string>();
      
        }
        public static bool AuthenticUser(string userId, ClaimsPrincipal requestingUser)
        {
            return Id(requestingUser) == userId;
        }
        
    }
}
