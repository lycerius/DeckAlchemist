namespace DeckAlchemist.WebApp.Api.Objects.CardDatabase.MTG
{
    public class MTGCard : ICard
    {
        public string Name {get; set;}
        public CCGType CCG {get; set;}
        public string Image { get; set; }
    }
}