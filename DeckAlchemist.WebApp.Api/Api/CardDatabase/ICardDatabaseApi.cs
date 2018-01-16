
using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects;

namespace DeckAlchemist.WebApp.Api.Api 
{
    public interface ICardDatabase
    {
        IEnumerable<Card> GetAllCards();
    }
}
