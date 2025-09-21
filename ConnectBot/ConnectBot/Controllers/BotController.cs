using ConnectBot.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Telegram.Bot.Types;

namespace ConnectBot.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : Controller
    {
        private readonly ICommandUpdateHandler _updateHandler;
        private readonly string _secretToken;

        public BotController(IConfiguration configuration, ICommandUpdateHandler commandUpdateHandler)
        {
            _updateHandler = commandUpdateHandler;
            _secretToken = configuration.GetValue<string>("SECRET_TOKEN") ??
                           Environment.GetEnvironmentVariable("SECRET_TOKEN") ?? "";
            if (string.IsNullOrEmpty(_secretToken))
            {
                throw new ArgumentException("Secret token cannot be empty");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Update update)
        {
            Request.Headers.TryGetValue("X-Telegram-Bot-Api-Secret-Token", out StringValues signature);
            if (string.IsNullOrEmpty(signature) || signature != _secretToken)
            {
                return Unauthorized("Unauthorized Request");
            }

            await _updateHandler.Handle(update);

            return Ok();
        }
    }
}
