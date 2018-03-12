using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Collection
{
    public class Collection : ICollection
    {
        public string UserId { get; set; }
        public List<IOwnedCard> OwnedCards { get; set; }
        public List<IBorrowedCard> BorrowedCards { get; set; }
    }
}
