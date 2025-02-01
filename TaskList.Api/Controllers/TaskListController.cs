using Microsoft.AspNetCore.Mvc;
using TaskList.Application.Commands;
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
            var query = new GetTaskListQuery { Id = id };
            var result = await _service.GetTaskListAsync(query);
            
            if (result == null)
                return NotFound();
            
            return Ok(result);
        }
        
        [HttpPost("{taskListId}/share")]
        public async Task<IActionResult> AddRelation(string taskListId, [FromBody] AddTaskListRelationCommand command)
        {
            if (taskListId != command.TaskListId)
                return BadRequest("TaskListId mismatch.");
            
            await _service.AddTaskListRelationAsync(command);
            return NoContent();
        }
        
        [HttpPut("{taskListId}")]
        public async Task<IActionResult> UpdateTaskList(string taskListId, [FromBody] UpdateTaskListCommand command)
        {
            if (taskListId != command.TaskListId)
                return BadRequest("TaskListId in URL does not match the request body.");
            
            await _service.UpdateTaskListAsync(command);
            return NoContent();
        }
        
        [HttpDelete("{taskListId}")]
        public async Task<IActionResult> DeleteTaskList(string taskListId, [FromQuery] string currentUserId)
        {
            var command = new DeleteTaskListCommand
            {
                TaskListId = taskListId,
                CurrentUserId = currentUserId
            };
            await _service.DeleteTaskListAsync(command);
            return NoContent();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTaskLists([FromQuery] GetTaskListsQuery query)
        {
            var result = await _service.GetTaskListsAsync(query);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTaskList([FromBody] CreateTaskListCommand command)
        {
            var createdTaskList = await _service.CreateTaskListAsync(command);
            return CreatedAtAction(nameof(GetTaskListById), new { id = createdTaskList.Id }, createdTaskList);
        }
        
        [HttpGet("{taskListId}/relations")]
        public async Task<IActionResult> GetTaskListRelations(string taskListId, [FromQuery] string currentUserId)
        {
            var query = new GetTaskListRelationsQuery
            {
                TaskListId = taskListId,
                CurrentUserId = currentUserId
            };
            var relations = await _service.GetTaskListRelationsAsync(query);
            return Ok(relations);
        }
        
        [HttpDelete("{taskListId}/relations/{sharedUserId}")]
        public async Task<IActionResult> RemoveTaskListRelation(
            string taskListId,
            string sharedUserId,
            [FromQuery] string currentUserId)
        {
            var command = new RemoveTaskListRelationCommand
            {
                TaskListId = taskListId,
                SharedUserId = sharedUserId,
                CurrentUserId = currentUserId
            };
            await _service.RemoveTaskListRelationAsync(command);
            return NoContent();
        }
    }
}