using System;
namespace DeckAlchemist.Api.Objects.Cards.Mtg
{
    public class MtgLegality : IMtgLegality
    {
        public string Format { get; set; }
        public string Legality { get; set; }
    }
}
