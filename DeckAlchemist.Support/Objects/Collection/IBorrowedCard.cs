using System;
using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Collection
{
    public interface IBorrowedCard
    {
        string CardId { get; set; }
        int AmountBorrowed { get; set; }
        IDictionary<string, int> Borrows { get; set; }
    }
}
