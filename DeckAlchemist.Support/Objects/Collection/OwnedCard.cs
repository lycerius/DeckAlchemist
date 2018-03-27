using System;
using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Collection
{
    public class OwnedCard : IOwnedCard
    {
        public string CardId { get; set; }
        public int TotalAmount { get; set; }
        public int Available { get; set; }
        public IDictionary<string, int> InDecks { get; set; }
        public IDictionary<string, int> LentTo { get; set; }
    }
}
