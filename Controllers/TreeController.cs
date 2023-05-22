using _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Controllers
{
    [ApiController]
    [Route("api/user/tree")]
    public class TreeController : ControllerBase
    {
        private readonly ITreeService _treeService;

        public TreeController(ITreeService treeService)
        {
            _treeService = treeService;
        }

        [HttpGet("get")]
        public IActionResult GetTree(string treeName)
        {
            var tree = _treeService.GetTree(treeName);
            return Ok(tree);
        }
    }
}
