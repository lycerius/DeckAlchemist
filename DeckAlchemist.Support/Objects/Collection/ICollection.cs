using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Collection
{
    public interface ICollection
    {
        string UserId { get; set; }
        string CollectionId { get; set; }
        List<IOwnedCard> OwnedCards { get; set; }
        List<IBorrowedCard> BorrowedCards { get; set; }
    }
}
