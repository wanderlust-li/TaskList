using TaskList.Application.DTO;
using TaskList.Application.Queries;

namespace TaskList.Application.Interfaces;

public interface ITaskListService
{
    Task<TaskListDto?> GetTaskListAsync(GetTaskListQuery query, CancellationToken cancellationToken = default);
}