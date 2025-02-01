using TaskList.Application.Commands;
using TaskList.Application.DTO;
using TaskList.Application.Exceptions;
using TaskList.Application.Interfaces;
using TaskList.Application.Queries;
using TaskList.Domain.DTO;
using TaskList.Domain.Interfaces;

namespace TaskList.Application.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _repository;

        public TaskListService(ITaskListRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskListDto?> GetTaskListAsync(GetTaskListQuery query, CancellationToken cancellationToken = default)
        {
            var taskList = await _repository.GetByIdAsync(query.Id, cancellationToken);
            if (taskList == null)
                return null;
            
            var dto = new TaskListDto
            {
                Id = taskList.Id,
                Name = taskList.Name,
                OwnerId = taskList.OwnerId,
                CreatedDateTime = taskList.CreatedDateTime,
                SharedWith = taskList.SharedWith
            };

            return dto;
        }

        public async Task AddTaskListRelationAsync(AddTaskListRelationCommand command, CancellationToken cancellationToken = default)
        {
            var taskList = await _repository.GetByIdAsync(command.TaskListId, cancellationToken);
            if (taskList == null)
                throw new TaskListException("Task list not found.");
            
            if (taskList.OwnerId != command.CurrentUserId && !taskList.SharedWith.Contains(command.CurrentUserId))
                throw new TaskListException("Not authorized to add relation to this task list.");
            
            await _repository.AddSharedUserAsync(command.TaskListId, command.SharedUserId, cancellationToken);
        }
        
        public async Task UpdateTaskListAsync(UpdateTaskListCommand command, CancellationToken cancellationToken = default)
        {
            var taskList = await _repository.GetByIdAsync(command.TaskListId, cancellationToken);
            if (taskList == null)
                throw new TaskListException("Task list not found.");
            
            if (taskList.OwnerId != command.CurrentUserId && !taskList.SharedWith.Contains(command.CurrentUserId))
                throw new TaskListException("Not authorized to update this task list.");
            
            taskList.Name = command.NewName;
            
            await _repository.UpdateAsync(taskList, cancellationToken);
        }
        
        public async Task DeleteTaskListAsync(DeleteTaskListCommand command, CancellationToken cancellationToken = default)
        {
            var taskList = await _repository.GetByIdAsync(command.TaskListId, cancellationToken);
            if (taskList == null)
                throw new TaskListException("Task list not found.");
            
            if (taskList.OwnerId != command.CurrentUserId)
                throw new TaskListException("Not authorized to delete this task list. Only the owner can delete it.");
            
            await _repository.DeleteAsync(command.TaskListId, cancellationToken);
        }
        
        public async Task<List<TaskListMinimalDto>> GetTaskListsAsync(GetTaskListsQuery query, CancellationToken cancellationToken = default)
        {
            var taskLists = await _repository.GetTaskListsAsync(query.CurrentUserId, query.PageNumber, query.PageSize, cancellationToken);

            var result = taskLists.Select(t => new TaskListMinimalDto
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            return result;
        }
        
        public async Task<TaskListDto> CreateTaskListAsync(CreateTaskListCommand command, CancellationToken cancellationToken = default)
        {
            var newTaskList = new Domain.Entities.TaskList
            {
                Name = command.Name,
                OwnerId = command.OwnerId,
                CreatedDateTime = DateTime.UtcNow,
                SharedWith = new List<string>()
            };
            
            await _repository.CreateAsync(newTaskList, cancellationToken);
            
            return new TaskListDto
            {
                Id = newTaskList.Id,
                Name = newTaskList.Name,
                OwnerId = newTaskList.OwnerId,
                CreatedDateTime = newTaskList.CreatedDateTime,
                SharedWith = newTaskList.SharedWith
            };
        }
        
        public async Task<List<string>> GetTaskListRelationsAsync(GetTaskListRelationsQuery query, CancellationToken cancellationToken = default)
        {
            var taskList = await _repository.GetByIdAsync(query.TaskListId, cancellationToken);
            if (taskList == null)
                throw new TaskListException("Task list not found.");
            
            if (taskList.OwnerId != query.CurrentUserId && !taskList.SharedWith.Contains(query.CurrentUserId))
                throw new TaskListException("Not authorized to view relations for this task list.");
            
            return taskList.SharedWith;
        }
        
        public async Task RemoveTaskListRelationAsync(RemoveTaskListRelationCommand command, CancellationToken cancellationToken = default)
        {
            var taskList = await _repository.GetByIdAsync(command.TaskListId, cancellationToken);
            if (taskList == null)
                throw new TaskListException("Task list not found.");
            
            if (taskList.OwnerId != command.CurrentUserId && !taskList.SharedWith.Contains(command.CurrentUserId))
                throw new TaskListException("Not authorized to remove relation from this task list.");
            
            await _repository.RemoveSharedUserAsync(command.TaskListId, command.SharedUserId, cancellationToken);
        }
    }
}
