namespace DeckAlchemist.WebApp.Api.Objects.Deck.MTG
{
    public class MTGDeckCard : IDeckCard
    {
        public string Name {get; set;}
        public int Count {get; set;}
    }
}