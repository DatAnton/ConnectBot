namespace ConnectBot.Application.Constants
{
    public static class TextConstants
    {
        public const string PrivacyPolicyText =
            $"Привет!{EmojiConstants.HelloEmoji}\r\nЧтобы обеспечить работу нашего бота, мы собираем ваши данные, такие как имя, фамилия и никнейм в Telegram.\r\nИспользование бота означает ваше согласие с нашей политикой конфиденциальности.";

        public const string WelcomeBotText =
            $"Меня зовут Nekko.{EmojiConstants.RobotEmoji}\r\nЯ виртуальный робот молодежной команды.{EmojiConstants.TeamEmoji}\r\n\r\nОчень хочу дружить с тобой {EmojiConstants.WowEmoji} \r\nЗалетай к нам, пользуйся кнопками внизу{EmojiConstants.DownEmoji} и наслаждайся.{EmojiConstants.HeartEmoji}";
        public const string WrongCommandOrMessageText = $"{EmojiConstants.ExclamationMarkEmoji} Упс! Неправильная команда или сообщение. Используй кнопки внизу{EmojiConstants.DownEmoji}";
        public const string ExceptionOccuredText = $"{EmojiConstants.ExclamationMarkEmoji} Упс! Произошла ошибка. Обратитесь к организаторам.";

        public const string CommunicationRequestResponseText = $"{EmojiConstants.HugsEmoji} Мы приняли твой запрос! Скоро кто-нибудь подойдет тебя обнять. Не убегай! {EmojiConstants.LaughingEmoji}";
        public const string CommunicationRequestAlreadyExistsResponseText = $"{EmojiConstants.HugsEmoji} Ты уже создал запрос на сегодня! Скоро кто-нибудь подойдет тебя обнять. Не убегай! {EmojiConstants.LaughingEmoji}";
        public static Func<string, string> CommunicationRequestHandlerText = author => $"{EmojiConstants.ExclamationMarkEmoji} Запрос на объятия от {author}";

        public static string FeedbackText = $"Здесь можно написать сообщение, фидбэк или идеи команде. Кликни на поле ввода, чтобы начать ввод текста. {EmojiConstants.DownEmoji}";
        public static string FeedbackResponseText = $"Большое спасибо, что связался с нами. Твое мнение ценно для нас {EmojiConstants.HeartEmoji}";
        public static Func<string, string, string> NewFeedbackHandlerText = (author, text) => $"{EmojiConstants.ExclamationMarkEmoji} Новый фидбек от {author}: \r\n{text}";

        public static string SetManualCheckInModeText = "Добавь учасника вручную. Напиши его имя и фамилию через пробел.";

        public static string NotFoundTodayEventText = $"{EmojiConstants.SadSmileEmoji} Нет событий сегодня";
        public static string IceBreakerAlreadyGeneratedText = $"{EmojiConstants.DoneEmoji} Ice Breaker уже сгенерирован";
        public static string IceBreakerGeneratedSuccessfullyText = $"{EmojiConstants.DoneEmoji} Ice Breaker сгенерирован успешно!";
        public static string CheckInRequiredForIceBreakerText = $"{EmojiConstants.ExclamationMarkEmoji} Для Ice Breaker генерации ты должен зачекиниться!";
        public static string AlreadyCheckedInText = $"Ты уже зачекинился. Наслаждайся там! {EmojiConstants.WowEmoji}";
        public static Func<string, string, string, string> CheckedInText = (uniqueNumber, teamColor, connectName) => $"Добро пожаловать на {connectName}\r\n\r\n{EmojiConstants.DoneEmoji}Ты зачекинился. Спасибо!\r\n\r\nТвой номер - {uniqueNumber}\r\nЦвет команды - {teamColor}\r\n\r\nНаслаждайся там! {EmojiConstants.WowEmoji}";

        public static Func<string, string, string> AllParticipationsText = (eventName, usersList) => $"Все участники {eventName}: \r\n\r\n{usersList}";

        public const string OkText = $"Ок{EmojiConstants.OkEmoji}";
        public const string NoText = "Нет";

        public const string SocialNetworksText = $"Вот все наши социальные сети. Подписывайся и будь в курсе событий! {EmojiConstants.PhoneEmoji}";
        public const string SocialNetworksInstagramText = $"{EmojiConstants.CameraEmoji} Instagram";
        public const string SocialNetworksTelegramNewsText = $"{EmojiConstants.NoteEmoji} Sila Youth News";
        public const string SocialNetworksSiteText = $"{EmojiConstants.GlobeEmoji} Sila web-site";
        public const string SocialNetworksPhotosChannelText = $"{EmojiConstants.PictureEmoji} Sila Church PHOTO";

        public const string DonateToYouthTeamText =
            $"Спасибо, что решил пожертвовать молодежной команде.{EmojiConstants.WowEmoji}{EmojiConstants.HugsEmoji}\r\nЭто можно сделать платежом на банковские реквизиты церкви с отметкой <b>Youth</b>.\r\nМы любим тебя!{EmojiConstants.RedHeartEmoji}\r\n\r\nПолучатель: SILA CHURCH\r\nНомер счета: FI6571401420046462\r\nMessage: Youth";

        public static Func<string, string> IceBreakerMessageText = partner => $"{EmojiConstants.IceEmoji}{EmojiConstants.RedCrossEmoji} It's Ice breaker time!\r\n\r\nПора разломать лед между нами и стать ближе.\r\nТвой собеседник сегодня - {partner}\r\n Найди его и вперед общаться{EmojiConstants.TeamEmoji}";
        public static Func<string, string, string> IceBreakerListText = (eventName, usersList) => $"Ice breaker {eventName}: \r\n\r\n{usersList}";
    }
}
