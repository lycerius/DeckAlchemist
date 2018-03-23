using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Messages
{
    public class MessageBox : IMessageBox
    {
        public string UserId { get; set; }
        public IDictionary<string, IMessage> Messages { get; set; }
    }
}
