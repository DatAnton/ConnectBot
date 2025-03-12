using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Event;
using ConnectBot.Application.Main;
using ConnectBot.Application.Menu;
using ConnectBot.Application.Models;
using ConnectBot.Application.Users;
using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using MediatR;
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
        private readonly ITelegramBotService _botService;

        public CommandUpdateHandler(IMediator mediator, UserCache userCache, IApplicationDbContext dbContext,
            ITelegramBotService botService)
        {
            _mediator = mediator;
            _userCache = userCache;
            _dbContext = dbContext;
            _botService = botService;
        }

        private readonly Dictionary<string, MessageCommand> _router = new()
        {
            {
                CommandConstants.StartCommand, new Start.Command()
            },
            {
                CommandConstants.SocialNetworksCommand, new SocialNetworks.Command()
            },
            {
                CommandConstants.CommunicationRequestCommand, new CommunicationRequest.Command()
            },
            {
                CommandConstants.FeedbackCommand, new Feedback.Command()
            },
            {
                CommandConstants.CheckInCommand, new CheckIn.Command()
            },
            {
                CommandConstants.AllParticipationsCommand, new Participatings.Command()
            },
            {
                CommandConstants.DonateYouthTeamCommand, new DonateToYouthTeam.Command()
            },
            {
                CommandConstants.IceBreakerCommand, new IceBreaker.Command()
            },
            {
                CommandConstants.AllParticipationsNumbersCommand, new ParticipatingsNumber.Command()
            },
            {
                CommandConstants.AdminPanelCommand, new AdminPanel.Command()
            },
            {
                CommandConstants.UserPanelCommand, new UserPanel.Command()
            },
            {
                CommandConstants.QuestionsCommand, new Questions.Command()
            }
        };


        public async Task Handle(Update update)
        {
            //ToDo: Add error handler
            try
            {
                if (update.Message != null)
                {
                    await _botService.SetClientLoading(update.Message.Chat.Id);
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
                    StackTrace = ex.StackTrace,
                    CreatedAt = DateTime.UtcNow,
                    MessageText = update.Message?.Text,
                    ChatId = update.Message?.Chat.Id,
                });
                await _dbContext.SaveChangesAsync(CancellationToken.None);
                await _botService.SendMessage(update.Message.Chat.Id, TextConstants.ExceptionOccuredText);
            }
        }

        private async Task HandleMessage(Message message)
        {
            if (message is not { Type: MessageType.Text } || string.IsNullOrEmpty(message.Text))
            {
                await _mediator.Send(new WrongCommand.Command().SetMessage(message));
                return;
            }

            if (_userCache.IsUserInTypeMode(message.From?.Id))
            {
                if (!_router.ContainsKey(message.Text))
                {
                    if (_userCache.IsUserInMode(message.From?.Id, UserState.FeedbackMode))
                    {
                        await _mediator.Send(new CreateFeedback.Command().SetMessage(message));
                    }
                    _userCache.SetUserMode(message.From?.Id, UserState.None);
                    return;
                }
                _userCache.SetUserMode(message.From?.Id, UserState.None);
            }

            if (_router.TryGetValue(message.Text, out MessageCommand command))
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
