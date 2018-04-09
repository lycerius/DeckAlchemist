using System;
using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Collection
{
    public interface IOwnedCard
    {
        string CardId { get; set; }
        int TotalAmount { get; set; }
        int Available { get; set; }
        bool Lendable { get; set; }
        IDictionary<string, int> InDecks { get; set; }
        IDictionary<string, int> LentTo { get; set; }
    }
}
