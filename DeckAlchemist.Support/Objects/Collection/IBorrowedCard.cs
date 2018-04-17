namespace DeckAlchemist.Support.Objects.Collection
{
    public interface IBorrowedCard
    {
        string CardId { get; set; }
        string Lender { get; set; }
        int AmountBorrowed { get; set; }
    }
}
