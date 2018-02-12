using System;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Mtg.Cards;

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

        public static MtgCard FromMongo(MongoMtgCard card)
        {
            return new MtgCard()
            {
                ManaCost = card.ManaCost,
                Cmc = card.Cmc,
                Colors = card.Colors,
                Type = card.Type,
                Types = card.Types,
                SubTypes = card.SubTypes,
                Text = card.Text,
                Power = card.Power,
                Toughness = card.Toughness,
                Layout = card.Layout,
                ImageName = card.ImageName,
                Name = card.Name,
                Legality = card.Legality
            };
        }

        protected bool Equals(MtgCard other)
        {
            return string.Equals(ManaCost, other.ManaCost) 
                   && Equals(Types, other.Types) 
                   && Equals(SubTypes, other.SubTypes) 
                   && string.Equals(Text, other.Text)
                   && string.Equals(Name, other.Name) 
                   && Equals(Legality, other.Legality);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MtgCard) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ManaCost != null ? ManaCost.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Types != null ? Types.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SubTypes != null ? SubTypes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Legality != null ? Legality.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
