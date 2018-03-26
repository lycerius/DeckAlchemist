using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.UserDeck
{
    public interface IUserDeck
    {
        string UserId { get; set; }
        string DeckId { get; set; }
        string DeckName { get; set; }
        IDictionary<string, int> CardsAndAmounts { get; set; }
    }
}
