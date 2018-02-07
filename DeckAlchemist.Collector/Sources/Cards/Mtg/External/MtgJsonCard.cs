using System;
using System.Collections.Generic;
using DeckAlchemist.Collector.Objects.Cards;
using Newtonsoft.Json;
using System.Linq;
namespace DeckAlchemist.Collector.Sources.Cards.Mtg.External
{
    public class MtgJsonCard
    {


        public string ManaCost { get; set; }
        public string Cmc { get; set; }
        public string[] Colors { get; set; }
        public string Type { get; set; }
        public string[] Types { get; set; }
        public string[] SubTypes { get; set; }
        public string Text { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string Layout { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        [JsonProperty("legalities")]
        public MtgJsonLegality[] Legality { get; set; }

        public static IMtgCard ToMtgCard(MtgJsonCard card)
        {
            return new MtgCard
            {
                
                Cmc = !string.IsNullOrEmpty(card.Cmc) ? double.Parse(card.Cmc) : 0D,
                Colors = card.Colors,
                ImageName = card.ImageName,
                Layout = card.Layout,
                ManaCost = card.ManaCost,
                Name = card.Name,
                Power = card.Power,
                SubTypes = card.SubTypes,
                Text = card.Text,
                Toughness = card.Toughness,
                Type = card.Type,
                Types = card.Types,
                Legality = card.Legality != null ?  card.Legality.Select(legal => ToMtgLegality(legal)).ToList() : null
            };
        }

        public static IMtgLegality ToMtgLegality(MtgJsonLegality legal)
        {
            return new MtgLegality
            {
                Format = legal.Format,
                Legality = legal.Legality
            };
        }
    }
}
