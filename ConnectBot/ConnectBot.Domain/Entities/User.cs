using System.Web;

namespace ConnectBot.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public long ChatId { get; set; }
        public bool IsAdmin { get; set; }
        public IList<Role> Roles { get; set; }
        public IList<CommunicationRequest> CommunicationRequests { get; set; }
        public IList<EventParticipation> EventParticipations { get; set; }

        public string DisplayName => HttpUtility.HtmlEncode($"{FirstName} {LastName} (@{UserName})");
    }
}
