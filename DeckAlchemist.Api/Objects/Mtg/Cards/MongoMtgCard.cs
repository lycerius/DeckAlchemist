using System.Collections.Generic;
using DeckAlchemist.Collector.Objects.Cards;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeckAlchemist.Api.Objects.Mtg.Cards
{
    public class MongoMtgCard : IMtgCard
    {
        [BsonId]
        public ObjectId _id { get; set; }
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
        [BsonIgnoreIfNull]

        public IEnumerable<IMtgLegality> Legality { get; set; }

        public static MongoMtgCard FromMtgCard(IMtgCard card)
        {
            return new MongoMtgCard
            {
                Cmc = card.Cmc,
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
                Legality = card.Legality
            };
        }
    }
}
