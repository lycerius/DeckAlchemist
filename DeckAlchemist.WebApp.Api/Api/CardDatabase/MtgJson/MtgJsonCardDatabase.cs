using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects;
using System.Net.Http;
using Newtonsoft.Json;
namespace DeckAlchemist.WebApp.Api.Api.MtgJson
{
    public class MtgJsonCardDatabase : ICardDatabase
    {
        private const string mtgJsonApiEndpoint = "https://mtgjson.com/json/";
        public IEnumerable<Card> GetAllCards()
        {
            var webClient = GetWebClient();
            var endPoint = $"{mtgJsonApiEndpoint}AllCards-x.json";
            using(var getTask = webClient.GetAsync(endPoint)){
                getTask.Wait();
                getTask.Result.EnsureSuccessStatusCode();
                var readTask = getTask.Result.Content.ReadAsStringAsync();
                readTask.Wait();
                var jsonCards = JsonConvert.DeserializeObject(readTask.Result) as List<MtgJsonCard>;
            }
        }

        private HttpClient GetWebClient() {
            return new HttpClient();
        }

        private IEnumerable<Card> ConvertToCardList(IEnumerable<MtgJsonCard> cards){
            
        }
    }
}