using _20aa05db_9a03_490b_94e8_785205bc0dd0.AppDb;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Entity;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Interfaces;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Shared;

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Impl
{
    public class JournalService : IJournalService
    {
        private readonly AppDbContext _dbContext;

        public JournalService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<JournalEvent> GetPagedJournalEvents(int skip, int take, JournalEventFilter filter)
        {
            IQueryable<JournalEvent> query = _dbContext.JournalEvents;

            if (filter != null)
            {
                if (filter.From.HasValue)
                {
                    query = query.Where(j => j.Timestamp >= filter.From.Value);
                }

                if (filter.To.HasValue)
                {
                    query = query.Where(j => j.Timestamp <= filter.To.Value);
                }

                if (!string.IsNullOrEmpty(filter.Search))
                {
                    query = query.Where(j => j.QueryParameters.Contains(filter.Search) ||
                                             j.BodyParameters.Contains(filter.Search) ||
                                             j.ExceptionStackTrace.Contains(filter.Search));
                }
            }

            return query
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public JournalEvent GetJournalEvent(int id)
        {
            return _dbContext.JournalEvents.FirstOrDefault(j => j.Id == id);
        }

        public void LogException(string queryParameters, string bodyParameters, string stackTrace)
        {
            var journalEvent = new JournalEvent
            {
                EventId = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                QueryParameters = queryParameters,
                BodyParameters = bodyParameters,
                ExceptionStackTrace = stackTrace
            };

            _dbContext.JournalEvents.Add(journalEvent);
            _dbContext.SaveChanges();
        }
    }
}
