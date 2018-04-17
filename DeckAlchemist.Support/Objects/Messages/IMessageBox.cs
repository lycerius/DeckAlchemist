using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Messages
{
    public interface IMessageBox
    {
        string UserId { get; set; }
        IDictionary<string,IMessage> Messages { get; set; }
    }
}
