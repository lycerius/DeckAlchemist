using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Messages;

namespace DeckAlchemist.Api.Contracts
{
    public class LoanRequestMessageContract
    {
        public string SenderId { get; set; }
        public string GroupId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IDictionary<string, int> RequestedCardsAndAmounts { get; set; }
        public string RecipientId { get; set; }

        public LoanRequestMessage ToLoanRequestMessage()
        {
            return new LoanRequestMessage
            {
                Body = Body,
                GroupId = GroupId,
                RecipientId = RecipientId,
                RequestedCardsAndAmounts = RequestedCardsAndAmounts,
                SenderId = SenderId,
                Subject = Subject
            };
        }
    }
}
