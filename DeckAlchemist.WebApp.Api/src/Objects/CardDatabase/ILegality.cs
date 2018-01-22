namespace DeckAlchemist.WebApp.Api.Objects.CardDatabase
{
    public interface ILegality
    {
        string Format { get; set; }
        string Status { get; set; }

        bool Equals(ILegality other);
    }
}