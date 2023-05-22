using _20aa05db_9a03_490b_94e8_785205bc0dd0.Entity;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Exceptions;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Controllers
{
    [ApiController]
    [Route("api/user/tree/node")]
    public class TreeNodeController : ControllerBase
    {
        private readonly ITreeNodeService _treeNodeService;
        private readonly IJournalService _journalService;

        public TreeNodeController(ITreeNodeService treeNodeService, IJournalService journalService)
        {
            _treeNodeService = treeNodeService;
            _journalService = journalService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNode(string treeName, int parentNodeId, string nodeName)
        {
            try
            {
                var createdNode = await _treeNodeService.CreateNodeAsync(treeName, parentNodeId, nodeName);
                return Ok(createdNode);
            }
            catch (SecureException ex)
            {
                _journalService.LogException(null, null, ex.StackTrace);
                var response = new
                {
                    type = "Secure",
                    id = ex.EventId,
                    data = new { message = ex.Message }
                };
                return StatusCode(500, response);
            }
            catch (Exception ex)
            {
                var eventId = Guid.NewGuid().ToString();
                _journalService.LogException(null, null, ex.StackTrace);
                var response = new
                {
                    type = "Exception",
                    id = eventId,
                    data = new { message = $"Internal server error ID = {eventId}" }
                };
                return StatusCode(500, response);
            }
        }
    

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteNode(string treeName, int nodeId)
        {
            try
            {
                await _treeNodeService.DeleteNodeAsync(treeName, nodeId);
                return NoContent();
            }
            catch (SecureException ex)
            {
                _journalService.LogException(null, null, ex.StackTrace);
                var response = new
                {
                    type = "Secure",
                    id = ex.EventId,
                    data = new { message = ex.Message }
                };
                return StatusCode(500, response);
            }
            catch (Exception ex)
            {
                var eventId = Guid.NewGuid().ToString();
                _journalService.LogException(null, null, ex.StackTrace);
                var response = new
                {
                    type = "Exception",
                    id = eventId,
                    data = new { message = $"Internal server error ID = {eventId}" }
                };
                return StatusCode(500, response);
            }
        }

        [HttpPut("rename")]
        public async Task<IActionResult> RenameNode(string treeName, int nodeId, string newName)
        {
            try
            {
                await _treeNodeService.RenameNodeAsync(treeName, nodeId, newName);
                return NoContent();
            }
            catch (SecureException ex)
            {
                _journalService.LogException(null, null, ex.StackTrace);
                var response = new
                {
                    type = "Secure",
                    id = ex.EventId,
                    data = new { message = ex.Message }
                };
                return StatusCode(500, response);
            }
            catch (Exception ex)
            {
                var eventId = Guid.NewGuid().ToString();
                _journalService.LogException(null, null, ex.StackTrace);
                var response = new
                {
                    type = "Exception",
                    id = eventId,
                    data = new { message = $"Internal server error ID = {eventId}" }
                };
                return StatusCode(500, response);
            }
        }
    }
}
