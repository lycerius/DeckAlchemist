using System;
namespace DeckAlchemist.Collector.Objects.Decks
{
    public interface IMtgDeckCard
    {
        string Name { get; set; }
        int Count { get; set; }
        float FeatureIndex { get; }
    }
}
