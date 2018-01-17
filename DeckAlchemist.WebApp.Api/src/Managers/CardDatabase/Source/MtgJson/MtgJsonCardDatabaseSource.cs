using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace DeckAlchemist.WebApp.Api.Managers.Source.MtgJson
{
    public class MtgJsonCardDatabaseSource : ICardDatabaseSource
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
                var jsonCards = JsonConvert.DeserializeObject<Dictionary<string, MtgJsonCard>>(readTask.Result);
                return ConvertToCardList(jsonCards);
            }
        }

        private HttpClient GetWebClient() {
            return new HttpClient();
        }

        private IEnumerable<Card> ConvertToCardList(IDictionary<string, MtgJsonCard> cards){
            List<Card> conversion = new List<Card>();
            foreach(var card in cards.Values) if(card != null)
                conversion.Add(ConvertToCard(card));

            return conversion;
        }

        private Card ConvertToCard(MtgJsonCard card) {
            return new Card {
                ColorIdentity = card.ColorIdentity,
                Colors = card.Colors,
                ConvertedManaCost = card.ConvertedManaCost,
                ImageName = card.ImageName,
                Legalities = card.Legalities == null ? null : card.Legalities.Select(legal => ConvertToLegality(legal)).ToArray(),
                ManaCost = card.ManaCost,
                Name = card.Name,
                Power = card.Power,
                Subtypes = card.Subtypes,
                Text = card.Text,
                Toughness = card.Toughness,
                Type = card.Type,
                Types = card.Types
            };
        }

        private Legality ConvertToLegality(MtgJsonLegality legal) {
            return new Legality {
                Format = legal.Format,
                Status = legal.Status
            };
        }
    }
}