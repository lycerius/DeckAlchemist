using MongoDB.Bson.Serialization.Attributes;

namespace DeckAlchemist.Support.Objects.Decks
{
    public class MtgDeckCard : IMtgDeckCard
    {
        public string Name { get; set; }
        public int Count { get; set; }
        
        [BsonIgnore]
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
            return !ReferenceEquals(null, obj) &&
                   (ReferenceEquals(this, obj) || obj.GetType() == this.GetType() && Equals((MtgDeckCard) obj));
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
