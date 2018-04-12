using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Cards
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
        public IEnumerable<MtgLegality> Legality { get; set; }

        public static MtgCard FromMtg(IMtgCard card) 
        {
            return new MtgCard
            {
                Cmc = card.Cmc,
                Colors = card.Colors,
                ImageName = card.ImageName,
                Layout = card.Layout,
                Legality = card.Legality,
                ManaCost = card.ManaCost,
                Name = card.Name,
                Power = card.Power,
                SubTypes = card.SubTypes,
                Text = card.Text,
                Toughness = card.Toughness,
                Type = card.Type,
                Types = card.Types
            };    
        }
    }
}
