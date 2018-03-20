using System;
using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Messages
{
    public interface IMessageBox
    {
        string UserId { get; set; }
        IEnumerable<IMessage> Messages { get; set; }
    }
}
