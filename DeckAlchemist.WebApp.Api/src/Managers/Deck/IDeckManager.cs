namespace DeckAlchemist.WebApp.Api.Managers.Deck
{
    public interface IDeckManager
    {
        DeckCollection GetAllDecks();

        DeckCollection GetAllDecksExtended();
    }
}