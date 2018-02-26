using System.Collections.Generic;
using System.Net.Http;
using DeckAlchemist.Support.Objects.Cards;
using Newtonsoft.Json;

namespace DeckAlchemist.Collector.Sources.Cards.Mtg.External
{
    public class MtgJsonExternalCardSource : IMtgExternalCardSource
    {
        const string mtgJsonApiEndpoint = "https://mtgjson.com/json/";

        public IEnumerable<IMtgCard> GetAllCards()
        {
            var webClient = new HttpClient();
            var endPoint = $"{mtgJsonApiEndpoint}AllCards-x.json";
            using (var getTask = webClient.GetAsync(endPoint))
            {
                getTask.Result.EnsureSuccessStatusCode();
                var readTask = getTask.Result.Content.ReadAsStringAsync();
                readTask.Wait();
                var jsonCards = JsonConvert.DeserializeObject<Dictionary<string, MtgJsonCard>>(readTask.Result);
                return ConvertToCardList(jsonCards);
            }
        }

        IEnumerable<IMtgCard> ConvertToCardList(IDictionary<string, MtgJsonCard> cards)
        {
            var dict = new List<IMtgCard>();
            foreach (var card in cards.Values)
                if (card != null)
                {
                    var converted = ConvertToCard(card);
                    dict.Add(converted);
                }

            return dict;
        }

        IMtgCard ConvertToCard(MtgJsonCard card)
        {
            return MtgJsonCard.ToMtgCard(card);
        }
    }
}
