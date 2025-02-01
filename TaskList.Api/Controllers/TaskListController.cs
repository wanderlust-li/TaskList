using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskList.Application.Interfaces;
using TaskList.Application.Queries;

namespace TaskList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskListController : ControllerBase
    {
        private readonly ITaskListService _service;

        public TaskListController(ITaskListService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskListById(string id)
        {
            var query = new GetTaskListQuery
            {
                Id = id,
            };

            var result = await _service.GetTaskListAsync(query);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}