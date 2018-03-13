using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.User
{
    public interface IUser
    {
        string UserId { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        string CollectionId { get; set; }
        List<string> Groups { get; set; }
        List<string> Decks { get; set; }
    }
}
