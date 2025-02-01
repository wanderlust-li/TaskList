using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using TaskList.Domain.Entities;
using TaskList.Domain.Interfaces;
using TaskList.Infrastructure.DatabaseContext;

namespace TaskList.Infrastructure.Repository
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly IMongoCollection<Domain.Entities.TaskList> _taskLists;

        public TaskListRepository(MongoDBContext context)
        {
            _taskLists = context.TaskLists;
        }

        public async Task<Domain.Entities.TaskList?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.TaskList>.Filter.Eq(t => t.Id, id);
            return await _taskLists.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task CreateAsync(Domain.Entities.TaskList taskList, CancellationToken cancellationToken = default)
        {
            await _taskLists.InsertOneAsync(taskList, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(Domain.Entities.TaskList taskList, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.TaskList>.Filter.Eq(t => t.Id, taskList.Id);
            await _taskLists.ReplaceOneAsync(filter, taskList, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.TaskList>.Filter.Eq(t => t.Id, id);
            await _taskLists.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task<List<Domain.Entities.TaskList>> GetTaskListsAsync(string currentUserId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.TaskList>.Filter.Or(
                Builders<Domain.Entities.TaskList>.Filter.Eq(t => t.OwnerId, currentUserId),
                Builders<Domain.Entities.TaskList>.Filter.AnyEq(t => t.SharedWith, currentUserId)
            );

            return await _taskLists.Find(filter)
                .SortByDescending(t => t.CreatedDateTime)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task AddSharedUserAsync(string taskListId, string userId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.TaskList>.Filter.Eq(t => t.Id, taskListId);
            var update = Builders<Domain.Entities.TaskList>.Update.AddToSet(t => t.SharedWith, userId);
            await _taskLists.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }

        public async Task<List<string>> GetSharedUsersAsync(string taskListId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.TaskList>.Filter.Eq(t => t.Id, taskListId);
            var taskList = await _taskLists.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return taskList?.SharedWith ?? new List<string>();
        }

        public async Task RemoveSharedUserAsync(string taskListId, string userId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.TaskList>.Filter.Eq(t => t.Id, taskListId);
            var update = Builders<Domain.Entities.TaskList>.Update.Pull(t => t.SharedWith, userId);
            await _taskLists.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }
    }
}
