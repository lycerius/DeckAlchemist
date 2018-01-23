using DeckAlchemist.WebApp.Api.Objects.Deck;

namespace DeckAlchemist.WebApp.Api.Managers.Deck.External
{
    public interface IExternalDeckSource
    {
         IDeck GetAllDecks();
    }
}