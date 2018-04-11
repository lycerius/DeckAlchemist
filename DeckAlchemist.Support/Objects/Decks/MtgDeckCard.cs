using MongoDB.Bson.Serialization.Attributes;

namespace DeckAlchemist.Support.Objects.Decks
{
    public class MtgDeckCard : IMtgDeckCard
    {
        public string Name { get; set; }
        public int Count { get; set; }
        
        [BsonIgnore]
        public float FeatureIndex => Name.GetHashCode();

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Count;
            }
        }
    }
}
