namespace ConnectBot.Application.Cache
{
    public enum UserState
    {
        None,
        FeedbackMode
    }

    public class UserCache
    {
        private Dictionary<long, UserState> _userModes = new();

        public bool IsUserInFeedbackMode(long? chatId)
        {
            return chatId.HasValue && _userModes.TryGetValue(chatId.Value, out UserState inFeedBackState) &&
                   inFeedBackState == UserState.FeedbackMode;
        }

        public void SetUserFeedbackMode(long? chatId, UserState state)
        {
            if(chatId.HasValue)
                _userModes[chatId.Value] = state;
        }
    }
}
