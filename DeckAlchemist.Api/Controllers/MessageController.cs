using System.Collections.Generic;
using System.Net.Http;
using DeckAlchemist.Api.Utility;
using DeckAlchemist.Api.Sources.Messages;
using DeckAlchemist.Support.Objects.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeckAlchemist.Api.Contracts;
using DeckAlchemist.Api.Sources.User;
using DeckAlchemist.Api.Sources.Group;
using DeckAlchemist.Api.Sources.Collection;

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/message")]
    public class MessageController : Controller
    {
        readonly IMessageSource _messageSource;
        readonly IUserSource _userSource;
        readonly IGroupSource _groupSource;
        readonly ICollectionSource _collectionSource;

        public MessageController(IMessageSource messageSource, IUserSource userSource, IGroupSource groupSource, ICollectionSource collectionSource)
        {
            _messageSource = messageSource;
            _userSource = userSource;
            _groupSource = groupSource;
            _collectionSource = collectionSource;
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<IMessage> GetUserMessages()
        {
            var userId = HttpContext.User.Id();
            return _messageSource.GetMessagesForUser(userId);
        }

        [Route("delete/{messageId}")]
        [HttpDelete]
        public void DeleteMessage(string messageId)
        {
            var userId = HttpContext.User.Id();
            _messageSource.DeleteMessage(userId, messageId);
        }

        [Route("send/user")]
        [HttpPost]
        public void SendMessageToUser([FromBody] UserMessageContract message)
        {
            var m = message.ToUserMessage();
            m.SenderId = HttpContext.User.Id();
            _messageSource.SendMessage(m);
        }

        [Route("send/loan")]
        [HttpPost]
        public void SendLoanRequestToUser([FromBody] LoanRequestMessageContract message)
        {
            var m = message.ToLoanRequestMessage();
            m.SenderId = HttpContext.User.Id();
            _messageSource.SendMessage(m);
        }

        [Route("send/invite")]
        [HttpPost]
        public IActionResult SendGroupInviteToUser([FromBody] GroupInviteContract message)
        {
            var user = _userSource.GetUserByUserName(message.RecipientUserName);
            if (user == null) return StatusCode(404);
            var m = message.ToGroupInviteMessage();
            var group = _groupSource.GetGroupInfo(message.GroupId);
            m.SenderId = HttpContext.User.Id();
            m.RecipientId = user.UserId;
            m.Subject = "Group Invite: " + group.GroupName;
            m.Body = "You have been invited to " + group.GroupName + ". Click 'Accept' to be added to this group!";
            _messageSource.SendMessage(m);

            return StatusCode(200);
        }

        [Route("accept/invite")]
        [HttpPost]
        public void AcceptGroupInvite([FromBody] string messageId)
        {
            var userId = HttpContext.User.Id();
            var message = _messageSource.GetMessageById(userId, messageId);
            if (message == null || message.Type != "Group") return;
            var groupInvite = message as GroupInviteMessage;
            var groupId = groupInvite.GroupId;
            _groupSource.AddUser(groupId, userId);
            var user = _userSource.Get(userId);
            user.Groups.Add(groupId);
            _userSource.Update(user);
            //TODO: groupInvite.Accepted = true;
            _messageSource.DeleteMessage(userId, groupInvite.MessageId);
        }

        [Route("accept/loan")]
        [HttpPost]
        public void AcceptLoanRequest([FromBody] string messageId)
        {
            var userId = HttpContext.User.Id();
            var message = _messageSource.GetMessageById(userId, messageId);
            if (message.Type != "Loan") return;
            var loanRequest = message as LoanRequestMessage;
            _collectionSource.MarkCardAsLent(loanRequest.RecipientId, loanRequest.SenderId, loanRequest.RequestedCardsAndAmounts);
            _collectionSource.AddCardAsLent(loanRequest.RecipientId, loanRequest.SenderId, loanRequest.RequestedCardsAndAmounts);
            _messageSource.DeleteMessage(userId, messageId);
        }

        
    }
}
