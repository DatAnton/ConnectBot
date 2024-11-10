namespace ConnectBot.Domain.Entities
{
    public class EventParticipation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int TeamColorId { get; set; }
        public TeamColor TeamColor { get; set; }
        public int UniqueNumber { get; set; }
        public DateTime CheckedInAt { get; set; }
    }
}
