using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Messages
{
    public class LoanRequestMessage : IMessage
    {
        public string Type => "Loan";
        public bool UnRead { get; set; }
        public string SenderId { get; set; }
        public string GroupId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Accepted { get; set; }
        public IEnumerable<string> RequestedRecipientCardIds { get; set; }
    }
}
