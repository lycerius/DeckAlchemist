namespace DeckAlchemist.Support.Objects.Cards
{
    public interface IMtgLegality
    {
        string Format { get; set; }
        string Legality { get; set; }
    }
}
