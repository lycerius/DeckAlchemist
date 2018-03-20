using System;
using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Messages
{
    public interface IMessageBox
    {
        string UserId { get; set; }
        IList<IMessage> Messages { get; set; }
    }
}
