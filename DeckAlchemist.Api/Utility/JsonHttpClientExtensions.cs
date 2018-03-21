using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeckAlchemist.Api.Utility
{
    public static class JsonHttpClientExtensions
    {
        public class JsonContent : StringContent
        {
            public JsonContent(object obj) : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"){}
        }

        public static HttpClient UseAuthToken(this HttpClient client, string idToken)
        {
            client.DefaultRequestHeaders.Add("Authorization", idToken);
            return client;
        }

        public static Task<HttpResponseMessage> PutObject(this HttpClient client, string requestUri, object obj)
        {
            return client.PutAsync(requestUri, new JsonContent(obj));
        }

        public static Task<HttpResponseMessage> PostObject(this HttpClient client, string requestUri, object obj)
        {
            return client.PostAsync(requestUri, new JsonContent(obj));
        }

    }
}
