using System;
namespace DeckAlchemist.Api.Contracts
{
    public class RemoveBorrowedCardsMessage
    {
        public string FromUser { get; set; }
        public string CardName { get; set; }
    }
}
