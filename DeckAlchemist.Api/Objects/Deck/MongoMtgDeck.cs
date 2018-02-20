using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeckAlchemist.Api.Objects.Deck
{
    public class MongoMtgDeck : IMtgDeck
    {

        [BsonId]
        public ObjectId _id;
        public string Name { get; set; }
        public double Meta { get; set; }
        public IDictionary<string, IMtgDeckCard> Cards { get; set; }
        public string id { get; set; }

        public static MongoMtgDeck FromMtgDeck(IMtgDeck deck)
        {
            return new MongoMtgDeck
            {
                Name = deck.Name,
                Cards = deck.Cards,
                Meta = deck.Meta
            };
        }
        
        public float CompareDecks(IMtgDeck other)
        {
            return DeckCompare.Compare(this, other);
        }

        protected bool Equals(MongoMtgDeck other)
        {
            return string.Equals(Name, other.Name) && Meta.Equals(other.Meta) && Equals(Cards, other.Cards);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MongoMtgDeck) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Meta.GetHashCode();
                hashCode = (hashCode * 397) ^ (Cards != null ? Cards.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
