namespace ConnectBot.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public string WelcomeMessage { get; set; }
        public int NumberOfTeams { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<EventParticipation> EventParticipations { get; set; }
        public IList<CommunicationRequest> CommunicationRequests { get; set; }
    }
}
