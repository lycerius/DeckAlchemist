using System.Linq;
using DeckAlchemist.WebApp.Api.Objects;
using Newtonsoft.Json;

//TODO: We will no longer need external sources once we get the integration service
namespace DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.External.MtgJson {
    public class MtgJsonCard {
        public string Name { get; set; }
        public string ManaCost { get; set; }

        [JsonProperty ("cmc")]
        public string ConvertedManaCost { get; set; }
        public string[] Colors { get; set; }
        public string Type { get; set; }
        public string[] Types { get; set; }
        public string[] Subtypes { get; set; }
        public string Text { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string ImageName { get; set; }
        public MtgJsonLegality[] Legalities { get; set; }
        public string[] ColorIdentity { get; set; }
    }
}