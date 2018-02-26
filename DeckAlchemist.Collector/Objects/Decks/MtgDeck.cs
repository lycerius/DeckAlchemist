using System;
using System.Collections.Generic;

namespace DeckAlchemist.Collector.Objects.Decks
{
    public class MtgDeck : IMtgDeck
    {
        public string Name { get; set; }
        public double Meta { get; set; }
        public string id { get; set; }
        public IDictionary<string, IMtgDeckCard> Cards { get; set; }
        
        public float CompareDecks(IMtgDeck other)
        {
            return DeckCompare.Compare(this, other);
        }

        protected bool Equals(IMtgDeck other)
        {
            return Math.Abs(CompareDecks(other)) < float.Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType() && !(obj is IMtgDeck)) return false;
            return Equals((IMtgDeck) obj);
        }

        public override int GetHashCode()
        {
            var space = DeckCompare.FeatureSpaceFor(this);
            return space.GetHashCode();
        }
    }
}
