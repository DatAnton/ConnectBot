namespace ConnectBot.Application
{
    public static class TextConstants
    {
        private const string HelloEmoji = "\U0001F44B";
        private const string HomeEmoji = "\U0001F3E0";
        private const string HugsEmoji = "\U0001F917";
        private const string RobotEmoji = "\U0001F916";
        private const string TeamEmoji = "\U0001F465";
        private const string HeartEmoji = "\U0001F499";
        private const string QuestionMarkEmoji = "\U00002753";
        private const string WowEmoji = "\U0001F929";
        private const string FingerDownEmoji = "\U00002B07";

        public const string PrivacyPolicyText =
            $"Привет!{HelloEmoji}\r\nЧтобы обеспечить работу нашего бота, мы собираем ваши данные, такие как имя, фамилия и никнейм в Telegram.\r\nИспользование бота означает ваше согласие с нашей политикой конфиденциальности.";

        public const string WelcomeBotText =
            $"Меня зовут Neko.{RobotEmoji}\r\nЯ виртуальный робот молодежной команды.{TeamEmoji}\r\n\r\nОчень хочу дружить с тобой {WowEmoji} \r\nЗалетай к нам, пользуйся кнопками внизу{FingerDownEmoji} и наслаждайся.{HeartEmoji}";
    }
}
