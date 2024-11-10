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
        public IList<Role> Roles { get; set; }
        public IList<CommunicationRequest> CommunicationRequests { get; set; }
        public IList<EventParticipation> EventParticipations { get; set; }
    }
}
