using System;
using System.Collections.Generic;
using System.Numerics;

namespace DeckAlchemist.Api.Objects.Mtg.Decks
{
    public class MtgDeck : IMtgDeck
    {
        public string Name { get; set; }
        public double Meta { get; set; }
        public string id { get; set; }
        public IDictionary<string, IMtgDeckCard> Cards { get; set; }

        public static MtgDeck FromMongo(MongoMtgDeck deck)
        {
            return new MtgDeck()
            {
                Name = deck.Name,
                Meta = deck.Meta,
                Cards = deck.Cards
            };
        }

        public float CompareDecks(IMtgDeck other)
        {
            return DeckCompare.Compare(this, other);
        }

        protected bool Equals(MtgDeck other)
        {
            return string.Equals(Name, other.Name) && Meta.Equals(other.Meta) && Equals(Cards, other.Cards);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MtgDeck) obj);
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
