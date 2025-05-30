﻿namespace ConnectBot.Application.Constants
{
    public static class CommandConstants
    {
        public const string StartCommand = "/start";
        public const string CheckInCommand = $"{EmojiConstants.HelloEmoji} Чек-ин";
        public const string SocialNetworksCommand = $"{EmojiConstants.GlobeEmoji} Социальные сети";
        public const string CommunicationRequestCommand = $"{EmojiConstants.HugsEmoji} Обнимите меня";
        public const string FeedbackCommand = $"{EmojiConstants.NoteEmoji} Связаться с командой";
        public const string AllParticipationsCommand = $"{EmojiConstants.ToolEmoji} Все учасники";
        public const string AllParticipationsNumbersCommand = $"{EmojiConstants.ToolEmoji}{EmojiConstants.NumbersEmoji} Все учасники(номера)";
        public const string DonateYouthTeamCommand = $"{EmojiConstants.RedHeartEmoji} Пожертвовать на развитие";
        public const string IceBreakerCommand = $"{EmojiConstants.IceEmoji}{EmojiConstants.RedCrossEmoji} Ice Breaker";
        public const string QuestionsCommand = $"{EmojiConstants.QuestionMarkEmoji} Анонимный вопрос";

        public const string AdminPanelCommand = "/admin_panel";
        public const string UserPanelCommand = "/user_panel";
    }
}
