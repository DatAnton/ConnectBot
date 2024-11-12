using MediatR;
using Telegram.Bot.Types;

namespace ConnectBot.Application.Models
{
    public class MessageCommand : IRequest
    {
        public Message Message { get; set; }

        public MessageCommand SetMessage(Message message)
        {
            Message = message;
            return this;
        }
    }
}
