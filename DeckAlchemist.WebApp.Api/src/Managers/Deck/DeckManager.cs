using DeckAlchemist.WebApp.Api.Managers.Deck.Source.External;

namespace DeckAlchemist.WebApp.Api.Managers.Deck
{
    public class DeckManager : IDeckManager
    {
        //TODO: Internal Source
        private readonly IExternalDeckSource _source;

        public DeckManager(IExternalDeckSource source)
        {
            _source = source;
        }
        
        public DeckCollection GetAllDecks()
        {
            return _source.GetAllDecks();
        }

        public DeckCollection GetAllDecksExtended() 
        {
            return _source.GetAllDecksExtended();
        }
    }
}