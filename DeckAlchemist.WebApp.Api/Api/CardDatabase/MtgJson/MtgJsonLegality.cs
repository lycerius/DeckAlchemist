using Newtonsoft.Json;

namespace DeckAlchemist.WebApp.Api.Api.MtgJson 
{
    public class MtgJsonLegality {
        public string Format {get;set;}
        
        [JsonProperty("legality")]
        public string Status {get;set;}
    }
}