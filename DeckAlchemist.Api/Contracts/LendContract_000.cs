using System.Collections.Generic;

namespace DeckAlchemist.Api.Contracts
{
    public class LendContract
    {
        public string Lender { get; set; }
        public string Lendee { get; set; }
        public IDictionary<string, int> CardsAndAmounts { get; set; }
    }
}
