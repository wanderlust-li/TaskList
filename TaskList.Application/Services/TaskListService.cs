using System.Threading;
using System.Threading.Tasks;
using TaskList.Application.DTO;
using TaskList.Application.Interfaces;
using TaskList.Application.Queries;
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
            {
                return null;
            }
            
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
    }
}