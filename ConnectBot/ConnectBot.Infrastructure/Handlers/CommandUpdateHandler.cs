using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Event;
using ConnectBot.Application.Main;
using ConnectBot.Application.Models;
using ConnectBot.Application.Users;
using ConnectBot.Domain.Interfaces;
using MediatR;
using System.Text.RegularExpressions;
using ConnectBot.Domain.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using CommunicationRequest = ConnectBot.Application.Event.CommunicationRequest;
using Feedback = ConnectBot.Application.Event.Feedback;

namespace ConnectBot.Infrastructure.Handlers
{
    public class CommandUpdateHandler : ICommandUpdateHandler
    {
        private readonly IMediator _mediator;
        private readonly UserCache _userCache;
        private readonly IApplicationDbContext _dbContext;

        public CommandUpdateHandler(IMediator mediator, UserCache userCache, IApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _userCache = userCache;
            _dbContext = dbContext;
        }

        private const string _emojisPattern =
            @"[\u1F600-\u1F64F\u1F300-\u1F5FF\u1F680-\u1F6FF\u1F700-\u1F77F\u1F780-\u1F7FF\u1F800-\u1F8FF\u1F900-\u1F9FF\u1FA00-\u1FA6F\u1FA70-\u1FAFF\u2600-\u26FF\u2700-\u27BF\u2300-\u23FF\u2B50\u1F4AC]";

        private static string GetSanitizedCommandName(string path)
        {
            return Regex.Replace(path, _emojisPattern, "").ToLower();
        }

        private readonly Dictionary<string, MessageCommand> _router = new()
        {
            {
                GetSanitizedCommandName(CommandConstants.StartCommand), new Start.Command()
            },
            {
                GetSanitizedCommandName(CommandConstants.SocialNetworksCommand), new SocialNetworks.Command()
            },
            {
                GetSanitizedCommandName(CommandConstants.CommunicationRequestCommand), new CommunicationRequest.Command()
            },
            {
                GetSanitizedCommandName(CommandConstants.FeedbackCommand), new Feedback.Command()
            },
            {
                GetSanitizedCommandName(CommandConstants.CheckInCommand), new CheckIn.Command()
            },
            {
                GetSanitizedCommandName(CommandConstants.AllParticipationsCommand), new Participatings.Command()
            },
            {
                GetSanitizedCommandName(CommandConstants.ManualCheckInCommand), new SetManualCheckInMode.Command()
            }
        };


        public async Task Handle(Update update)
        {
            //ToDo: Add error handler
            try
            {
                if (update.Message != null)
                {
                    await HandleMessage(update.Message);
                }
                else if (update.CallbackQuery != null)
                {
                    await HandleCallBackQuery(update.CallbackQuery);
                }
            }
            catch (Exception ex)
            {
                await _dbContext.Logs.AddAsync(new Log
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message,
                    CreatedAt = DateTime.UtcNow,
                    MessageText = update.Message?.Text,
                    ChatId = update.Message?.Chat?.Id,
                });
                await _dbContext.SaveChangesAsync(CancellationToken.None);
            }
        }

        private async Task HandleMessage(Message message)
        {
            if (message is not { Type: MessageType.Text } || string.IsNullOrEmpty(message.Text))
            {
                await _mediator.Send(new WrongCommand.Command().SetMessage(message));
                return;
            }

            if (_userCache.IsUserInMode(message.From?.Id, UserState.FeedbackMode)) 
            {
                if (!_router.ContainsKey(GetSanitizedCommandName(message.Text)))
                {
                    await _mediator.Send(new CreateFeedback.Command().SetMessage(message));
                    return;
                }
                _userCache.SetUserMode(message.From?.Id, UserState.None);
            }

            if (_userCache.IsUserInMode(message.From?.Id, UserState.ManualCheckInMode))
            {
                if (!_router.ContainsKey(GetSanitizedCommandName(message.Text)))
                {
                    await _mediator.Send(new ManualCheckIn.Command().SetMessage(message));
                    return;
                }
                _userCache.SetUserMode(message.From?.Id, UserState.None);
            }

            if (_router.TryGetValue(GetSanitizedCommandName(message.Text), out MessageCommand command))
            {
                await _mediator.Send(command.SetMessage(message));
            }
            else
            {
                await _mediator.Send(new WrongCommand.Command().SetMessage(message));
            }
        }

        private async Task HandleCallBackQuery(CallbackQuery query)
        {
            if (query.Data == UtilConstants.AcceptedPrivacyPolicyOk)
            {
                await _mediator.Send(new CreateUser.Command().SetCallbackQuery(query));
                await _mediator.Send(new Welcome.Command().SetMessage(query.Message));
            }
            else if (query.Data == UtilConstants.AcceptedPrivacyPolicyNo)
            {
                await _mediator.Send(new Start.Command().SetMessage(query.Message));
            }
        }
    }
}
