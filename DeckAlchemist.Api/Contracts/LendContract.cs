using System.Collections.Generic;

namespace DeckAlchemist.Api.Contracts
{
    public class LendContract
    {
        public string Lender { get; set; }
        public string Lendee { get; set; }
        public IEnumerable<string> CardNames { get; set; }
    }
}
