using _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Interfaces;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Shared;
using Microsoft.AspNetCore.Mvc;

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Controllers
{
    [ApiController]
    [Route("api/user/journal")]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost("getRange")]
        public IActionResult GetPagedJournalEvents(int skip = 0, int take = 10, [FromBody] JournalEventFilter filter = null)
        {
            var journalEvents = _journalService.GetPagedJournalEvents(skip, take, filter);
            return Ok(journalEvents);
        }

        [HttpGet("getSingle")]
        public IActionResult GetJournalEvent(int id)
        {
            var journalEvent = _journalService.GetJournalEvent(id);
            if (journalEvent == null)
                return NotFound();

            return Ok(journalEvent);
        }
    }
}
