using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;
using Newtonsoft.Json;

namespace DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.External.MtgJson {
    public class MtgJsonCardDatabaseSource : IExternalCardDatabaseSource {
        private const string mtgJsonApiEndpoint = "https://mtgjson.com/json/";
        public CardDatabaseCollection GetAllCards () {
            var webClient = GetWebClient ();
            var endPoint = $"{mtgJsonApiEndpoint}AllCards-x.json";
            using (var getTask = webClient.GetAsync (endPoint)) {
                getTask.Wait ();
                getTask.Result.EnsureSuccessStatusCode ();
                var readTask = getTask.Result.Content.ReadAsStringAsync ();
                readTask.Wait ();
                var jsonCards = JsonConvert.DeserializeObject<Dictionary<string, MtgJsonCard>> (readTask.Result);
                return new CardDatabaseCollection(ConvertToCardList (jsonCards));
            }
        }

        private HttpClient GetWebClient () {
            return new HttpClient ();
        }

        private Dictionary<string, ICard> ConvertToCardList (IDictionary<string, MtgJsonCard> cards) {
            var dict = new Dictionary<string, ICard> ();
            foreach (var card in cards.Values)
                if (card != null) {
                    var converted = ConvertToCard (card);
                    dict.Add (converted.Name, converted);
                }

            return dict;
        }

        private ICard ConvertToCard (MtgJsonCard card) {
            return new Card {
                    ColorIdentity = card.ColorIdentity,
                    Colors = card.Colors,
                    ConvertedManaCost = card.ConvertedManaCost,
                    ImageName = card.ImageName,
                    Legalities = card.Legalities == null ? null : card.Legalities.Select (legal => ConvertToLegality (legal)).ToArray (),
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

        private Legality ConvertToLegality (MtgJsonLegality legal) {
            return new Legality {
                Format = legal.Format,
                    Status = legal.Status
            };
        }
    }
}