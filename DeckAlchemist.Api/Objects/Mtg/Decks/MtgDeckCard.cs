namespace DeckAlchemist.Api.Objects.Mtg.Decks
{
    public class MtgDeckCard : IMtgDeckCard
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
