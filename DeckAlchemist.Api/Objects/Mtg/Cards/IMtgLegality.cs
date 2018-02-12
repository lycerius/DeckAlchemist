namespace DeckAlchemist.Api.Objects.Mtg.Cards
{
    
    public interface IMtgLegality
    {
        string Format { get; set; }
        string Legality { get; set; }
    }
}
