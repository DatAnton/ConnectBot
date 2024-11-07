namespace ConnectBot.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public long TelegramUserId { get; set; }
        public long ChatId { get; set; }
    }
}
