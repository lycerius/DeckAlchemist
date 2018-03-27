using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Collection
{
    public interface ICollection
    {
        string UserId { get; set; }
        string CollectionId { get; set; }
        IDictionary<string, IOwnedCard> OwnedCards { get; set; }
        IDictionary<string, IDictionary<string, IBorrowedCard>> BorrowedCards { get; set; }
    }
}
