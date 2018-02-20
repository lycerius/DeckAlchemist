namespace DeckAlchemist.Api.Objects.Mtg.Decks
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
    }
}
