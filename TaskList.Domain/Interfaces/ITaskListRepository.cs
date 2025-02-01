using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskList.Domain.Entities;

namespace TaskList.Domain.Interfaces
{
    public interface ITaskListRepository
    {
        Task<Entities.TaskList?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task CreateAsync(Entities.TaskList taskList, CancellationToken cancellationToken = default);
        Task UpdateAsync(Entities.TaskList taskList, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<List<Entities.TaskList>> GetTaskListsAsync(string currentUserId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task AddSharedUserAsync(string taskListId, string userId, CancellationToken cancellationToken = default);
        Task<List<string>> GetSharedUsersAsync(string taskListId, CancellationToken cancellationToken = default);
        Task RemoveSharedUserAsync(string taskListId, string userId, CancellationToken cancellationToken = default);
    }
}