using System;
namespace DeckAlchemist.Api.Objects.Cards.Mtg
{
    public interface IMtgLegality
    {
        string Format { get; set; }
        string Legality { get; set; }
    }
}
