using System;
using System.Collections.Generic;

namespace DeckAlchemist.Api.Objects.Mtg
{
    public interface IMtgCard
    {
        string Name { get; set; }
        string ManaCost { get; set; }
        int? Cmc { get; set; }
        IEnumerable<string> Colors { get; set; }
        string Type { get; set; }
        IEnumerable<string> SuperTypes { get; set; }
        IEnumerable<string> Types { get; set; }
        IEnumerable<string> SubTypes { get; set; }
        string Rarity { get; set; }
        string Text { get; set; }
        string Flavor { get; set; }
        string Artist { get; set; }
        string Number { get; set; }
        string Power { get; set; }
        string Toughness { get; set; }
        string Layout { get; set; }
        string MultiverseId { get; set; }
        string ImageName { get; set; }
        string Id { get; set; }
    }
}
