using System;
namespace DeckAlchemist.Api.Objects.Card.Mtg
{
    public interface IMtgLegality
    {
        string Format { get; set; }
        string Legality { get; set; }
    }
}
