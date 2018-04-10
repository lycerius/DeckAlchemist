using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;
using DeckAlchemist.Support.Objects.UserDeck;

namespace DeckAlchemist.Api.Contracts
{
    public class DeckModel
    {
        public IUserDeck UserDeck { get; set; }
        public IDictionary<string, IMtgCard> CardInfo;
    }
}