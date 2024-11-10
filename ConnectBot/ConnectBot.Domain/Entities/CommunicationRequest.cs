namespace ConnectBot.Domain.Entities
{
    public class CommunicationRequest
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int EventId { get;set; }
        public Event Event { get; set; }
    }
}
