namespace DeckAlchemist.Api.Objects.Mtg.Cards
{
    
    public class MtgLegality : IMtgLegality
    {
        public string Format { get; set; }
        public string Legality { get; set; }
    }
}
