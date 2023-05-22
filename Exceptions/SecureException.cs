namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Exceptions
{
    public class SecureException : Exception
    {
        public string EventId { get; }

        public SecureException(string message, string eventId) : base(message)
        {
            EventId = eventId;
        }
    }
}
