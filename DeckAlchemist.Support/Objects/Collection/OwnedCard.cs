using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckAlchemist.Support.Objects.Collection
{
    [BsonIgnoreExtraElements]
    public class OwnedCard : IOwnedCard
    {
        public string CardId { get; set; }
        public int TotalAmount { get; set; }
        [BsonIgnore]
        public int Available { get => TotalAmount - (LentTo != null ? LentTo.Sum(lent => lent.Value) : 0); set { } }
        public IDictionary<string, int> InDecks { get; set; }
        public IDictionary<string, int> LentTo { get; set; }
    }
}
