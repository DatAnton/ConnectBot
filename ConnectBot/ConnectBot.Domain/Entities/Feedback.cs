namespace ConnectBot.Domain.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
