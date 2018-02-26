using System;
using System.Collections.Generic;

namespace DeckAlchemist.Collector.Objects.Cards
{
    public class MtgCard : IMtgCard
    {
        public string ManaCost { get; set; }
        public double? Cmc { get; set; }
        public IEnumerable<string> Colors { get; set; }
        public string Type { get; set; }
        public IEnumerable<string> Types { get; set; }
        public IEnumerable<string> SubTypes { get; set; }
        public string Text { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string Layout { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public IEnumerable<IMtgLegality> Legality { get; set; }
    }
}
