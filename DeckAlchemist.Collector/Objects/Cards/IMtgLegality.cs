using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DeckAlchemist.Collector.Objects.Cards
{
    
    public interface IMtgLegality
    {
        string Format { get; set; }
        string Legality { get; set; }
    }
}
