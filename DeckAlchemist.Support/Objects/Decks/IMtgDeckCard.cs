namespace DeckAlchemist.Support.Objects.Decks
{
    public interface IMtgDeckCard
    {
        string Name { get; set; }
        int Count { get; set; }
        float FeatureIndex { get; }
    }
}
