using System;
using System.Collections.Generic;

namespace DeckAlchemist.Collector.Objects.Cards
{
    public interface IMtgCard
    {
        string ManaCost { get; set; }
        double? Cmc { get; set; }
        IEnumerable<string> Colors { get; set; }
        string Type { get; set; }
        IEnumerable<string> Types { get; set; }
        IEnumerable<string> SubTypes { get; set; }
        string Text { get; set; }
        string Power { get; set; }
        string Toughness { get; set; }
        string Layout { get; set; }
        string ImageName { get; set; }
        string Name { get; set; }
        IEnumerable<IMtgLegality> Legality { get; set; }
    }
}
