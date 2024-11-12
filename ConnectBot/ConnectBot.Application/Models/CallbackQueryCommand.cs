using MediatR;
using Telegram.Bot.Types;

namespace ConnectBot.Application.Models
{
    public class CallbackQueryCommand : IRequest
    {
        public CallbackQuery CallbackQuery { get; set; }

        public CallbackQueryCommand SetCallbackQuery(CallbackQuery callbackQuery)
        {
            CallbackQuery = callbackQuery;
            return this;
        }
    }
}
