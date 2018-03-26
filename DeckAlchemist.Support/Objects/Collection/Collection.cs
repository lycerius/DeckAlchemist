using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Collection
{
    public class Collection : ICollection
    {
        public string UserId { get; set; }
        public string CollectionId { get; set; }
        public IDictionary<string, IOwnedCard> OwnedCards { get; set; }
        public IDictionary<string, IBorrowedCard> BorrowedCards { get; set; }
    }
}
