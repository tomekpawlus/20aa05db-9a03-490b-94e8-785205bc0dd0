using _20aa05db_9a03_490b_94e8_785205bc0dd0.Entity;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Shared;

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Interfaces
{
    public interface IJournalService
    {
        public IEnumerable<JournalEvent> GetPagedJournalEvents(int skip, int take, JournalEventFilter? filter);
        JournalEvent GetJournalEvent(int id);
        void LogException(string queryParameters, string bodyParameters, string stackTrace);
    }
}
