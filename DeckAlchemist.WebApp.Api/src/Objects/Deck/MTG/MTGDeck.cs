namespace DeckAlchemist.WebApp.Api.Objects.Deck.MTG
{
    public class MTGDeck : IDeck
    {
        public string Name { get; set; }
        public CCGType CCG { get; set; }
        public double Meta { get; set; }
        public DeckCardCollection Cards { get; set; }
    }
}