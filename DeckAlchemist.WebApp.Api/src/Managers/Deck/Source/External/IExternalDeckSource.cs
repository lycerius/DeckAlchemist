namespace DeckAlchemist.WebApp.Api.Managers.Deck.Source.External
{
    public interface IExternalDeckSource
    {
         DeckCollection GetAllDecks();

         DeckCollection GetAllDecksExtended();
    }
}