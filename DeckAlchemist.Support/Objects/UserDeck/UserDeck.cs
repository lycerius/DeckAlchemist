using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.UserDeck
{
    public class UserDeck : IUserDeck
    {
        public string UserId { get; set; }
        public string DeckId { get; set; }
        public string DeckName { get; set; }
        public IDictionary<string, int> CardsAndAmounts { get; set; }
    }
}
