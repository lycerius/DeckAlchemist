using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeckAlchemist.Collector.Objects.Cards
{
    
    public class MtgLegality : IMtgLegality
    {
        public string Format { get; set; }
        public string Legality { get; set; }
    }
}
