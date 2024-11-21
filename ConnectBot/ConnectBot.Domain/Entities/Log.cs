namespace ConnectBot.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? InnerException { get; set; }
        public string? MessageText { get; set; }
        public long? ChatId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
