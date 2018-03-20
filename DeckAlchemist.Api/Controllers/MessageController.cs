using System.Collections.Generic;
using DeckAlchemist.Api.Auth;
using DeckAlchemist.Api.Sources.Messages;
using DeckAlchemist.Support.Objects.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/message")]
    public class MessageController : Controller
    {
        IMessageSource _messageSource;

        public MessageController(IMessageSource messageSource)
        {
            _messageSource = messageSource;
           
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<IMessage> GetUserMessages()
        {
            var userId = UserInfo.Id(HttpContext.User);
            return _messageSource.GetMessagesForUser(userId);
        }

        [Route("read")]
        [HttpPost]
        public void MarkMessageAsRead([FromBody] string messageId)
        {
            
        }

        [Route("send/user")]
        [HttpPost]
        public void SendMessageToUser([FromBody] UserMessage message)
        {
            _messageSource.SendMessage(message);
        }

        [Route("send/loan")]
        [HttpPost]
        public void SendLoanRequestToUser([FromBody] LoanRequestMessage message)
        {
            _messageSource.SendMessage(message);
        }

        [Route("send/invite")]
        [HttpPost]
        public void SendGroupInviteToUser([FromBody] GroupInviteMessage message)
        {
            _messageSource.SendMessage(message);
        }

        [Route("accept/invite")]
        [HttpPost]
        public void AcceptGroupInvite([FromBody] string messageId)
        {
            //TODO: Should add user to group in message
        }

        [Route("accept/loan")]
        [HttpPost]
        public void AcceptLoanRequest([FromBody] string messageId)
        {
            //TODO: Should accept the loan
        }

        
    }
}
