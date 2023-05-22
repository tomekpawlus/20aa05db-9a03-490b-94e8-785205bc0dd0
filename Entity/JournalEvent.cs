namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Entity
{
    public class JournalEvent
    {
        public int Id { get; set; }
        public Guid EventId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? QueryParameters { get; set; }
        public string? BodyParameters { get; set; }
        public string? ExceptionStackTrace { get; set; }
    }
}
