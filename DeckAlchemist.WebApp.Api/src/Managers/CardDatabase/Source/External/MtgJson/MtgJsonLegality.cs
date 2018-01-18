using Newtonsoft.Json;

namespace DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.External.MtgJson {
    public class MtgJsonLegality {
        public string Format { get; set; }

        [JsonProperty ("legality")]
        public string Status { get; set; }

    }
}