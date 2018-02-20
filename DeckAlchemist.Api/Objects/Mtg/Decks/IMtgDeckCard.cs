namespace DeckAlchemist.Api.Objects.Mtg.Decks
{
    public interface IMtgDeckCard
    {
        string Name { get; set; }
        int Count { get; set; }
        float FeatureIndex { get; }
    }
}
