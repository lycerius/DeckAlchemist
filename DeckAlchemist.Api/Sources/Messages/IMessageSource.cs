using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Messages;

namespace DeckAlchemist.Api.Sources.Messages
{
    public interface IMessageSource
    {
        IEnumerable<IMessage> GetMessagesForUser(string uId);
        IMessage GetMessageById(string userId, string messageId);
        void SendMessage(IMessage message);
        void SetMessageRead(string messageId, bool read);
        void AcceptGroupInvite(string messageId);
        void AcceptLoanRequest(string messageId);
        void Create(IMessageBox box);
        bool ExistsForUser(string uId);
        void Update(string userId, IMessage message);
    }
}
