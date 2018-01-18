namespace DeckAlchemist.WebApp.Api.Objects.Deck
{
    public class Deck : IDeck
    {
        public string Name { get; set; }
        public int Meta { get;set; }

        public ICardDeckCollection Cards {get;set;}
    }
}