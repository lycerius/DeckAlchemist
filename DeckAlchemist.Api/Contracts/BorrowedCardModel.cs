using System;
using DeckAlchemist.Support.Objects.Collection;

namespace DeckAlchemist.Api.Contracts
{
    public class BorrowedCardModel : IBorrowedCard
    {
        public string CardId { get; set; }
        public string Lender { get; set; }
        public int AmountBorrowed { get; set; }
        public string LenderUserName { get; set; }
    }
}
