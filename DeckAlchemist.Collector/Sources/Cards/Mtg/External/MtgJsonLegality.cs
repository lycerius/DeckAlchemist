using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DeckAlchemist.Collector.Sources.Cards.Mtg.External
{
    
    public class MtgJsonLegality
    {
        public string Format { get; set; }
        [JsonProperty("legality")]
        public string Legality { get; set; }
    }
}
