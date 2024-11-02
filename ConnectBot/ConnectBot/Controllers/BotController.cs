using ConnectBot.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConnectBot.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ICommandService _commandService;

        public BotController(ICommandService commandService, ITelegramBotClient telegramBotClient)
        {
            _commandService = commandService;
            _telegramBotClient = telegramBotClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Update update)
        {
            // TODO: set webhook secret_token while setting it
            //Request.Headers.TryGetValue("X-Telegram-Bot-Api-Secret-Token", out StringValues signature);
            //if (string.IsNullOrEmpty(signature) || signature != < Webhook - Secret - Token >)
            //{
            //    return Unauthorized("Unauthorized Request");
            //}
            // https://api.telegram.org/bot<Bot-Token>/setWebhook?url=<Webhook-Url>&secret_token=<Secret-Token>
            var message = update.Message;

            if (message == null)
            {
                return Ok();
            }

            foreach (var command in _commandService.Get())
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, _telegramBotClient);
                    break;
                }
            }
            return Ok();
        }
    }
}
