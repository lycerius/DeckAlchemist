using System;
namespace DeckAlchemist.Support.Objects.Collection
{
    public class BorrowedCard : IBorrowedCard
    {
        public string CardId { get; set; }
        public string Lender { get; set; }
        public int AmountBorrowed { get; set; }
    }
}
