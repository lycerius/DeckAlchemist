using System;
namespace DeckAlchemist.Api.Objects.Deck
{
    public class MtgDeckCard : IMtgDeckCard
    {
        public string Name { get; set; }
        public int Count { get; set; }
        
        public float FeatureIndex
        {
            get
            {
                //TODO Cluster all cards so similar cards are next to each other
                //and then give each card name an index according to the cluster
                //return featureset[Name];
                //but for now just use the hashcode of the name

                return Name.GetHashCode();
            }
        }

        protected bool Equals(MtgDeckCard other)
        {
            return string.Equals(Name, other.Name) && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MtgDeckCard) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Count;
            }
        }
    }
}
