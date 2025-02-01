using TaskList.Application.Commands;
using TaskList.Application.DTO;
using TaskList.Domain.DTO; 
using TaskList.Application.Queries;

namespace TaskList.Application.Interfaces
{
    public interface ITaskListService
    {
        Task<TaskListDto?> GetTaskListAsync(GetTaskListQuery query, CancellationToken cancellationToken = default);
        Task AddTaskListRelationAsync(AddTaskListRelationCommand command, CancellationToken cancellationToken = default);
        Task UpdateTaskListAsync(UpdateTaskListCommand command, CancellationToken cancellationToken = default);
        Task DeleteTaskListAsync(DeleteTaskListCommand command, CancellationToken cancellationToken = default);
        Task<List<TaskListMinimalDto>> GetTaskListsAsync(GetTaskListsQuery query, CancellationToken cancellationToken = default);
        Task<TaskListDto> CreateTaskListAsync(CreateTaskListCommand command, CancellationToken cancellationToken = default);
        Task<List<string>> GetTaskListRelationsAsync(GetTaskListRelationsQuery query, CancellationToken cancellationToken = default);
        Task RemoveTaskListRelationAsync(RemoveTaskListRelationCommand command, CancellationToken cancellationToken = default);
    }
}