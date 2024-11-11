using ConnectBot.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        private readonly string _secretToken;

        public BotController(ICommandService commandService, ITelegramBotClient telegramBotClient)
        {
            _commandService = commandService;
            _telegramBotClient = telegramBotClient;
            _secretToken = Environment.GetEnvironmentVariable("SECRET_TOKEN") ?? string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Update update)
        {
            Request.Headers.TryGetValue("X-Telegram-Bot-Api-Secret-Token", out StringValues signature);
            if (string.IsNullOrEmpty(signature) || signature != _secretToken)
            {
                return Unauthorized("Unauthorized Request");
            }
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
