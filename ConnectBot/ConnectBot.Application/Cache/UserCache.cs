using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;

namespace ConnectBot.Application.Cache
{
    public enum UserState
    {
        None,
        FeedbackMode
    }

    public class UserCache
    {
        private readonly IUserService _userService;
        private Dictionary<long, UserState> _userModes = new();
        public Dictionary<long, User> _users = new();
        private Dictionary<int, long> _admins = new();

        public UserCache(IUserService userService)
        {
            _userService = userService;
        }

        public bool IsUserInMode(long? chatId, UserState userState)
        {
            return chatId.HasValue && _userModes.TryGetValue(chatId.Value, out UserState inState) &&
                   inState == userState;
        }

        public bool IsUserInTypeMode(long? chatId)
        {
            return chatId.HasValue && _userModes.TryGetValue(chatId.Value, out UserState inState) &&
                   inState != UserState.None;
        }

        public void SetUserMode(long? chatId, UserState state)
        {
            if(chatId.HasValue)
                _userModes[chatId.Value] = state;
        }

        public async Task<User?> GetUserByChatId(long chatId, CancellationToken cancellationToken)
        {
            if (_users.TryGetValue(chatId, out var cachedUser)) return cachedUser;

            cachedUser = await _userService.GetUserByChatId(chatId, cancellationToken);
            if (cachedUser != null)
            {
                _users[chatId] = cachedUser;
            }
            return cachedUser;

        }

        public async Task<Dictionary<int, long>> GetAdmins(CancellationToken cancellationToken)
        {
            if (_admins.Count == 0)
            {
                _admins = (await _userService.GetUserAdmins(cancellationToken)).ToDictionary(k => k.Id, x => x.ChatId);
            }

            return _admins;
        }

        public async Task<bool> IsAdminChat(long? chatId)
        {
            if (!chatId.HasValue)
                return false;

            if (_admins.Count == 0)
            {
                await GetAdmins(CancellationToken.None);
            }

            return _admins.ContainsValue(chatId.Value);
        }
    }
}
