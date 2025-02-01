using MongoDB.Driver;
using TaskList.Infrastructure.Configuration;

namespace TaskList.Infrastructure.DatabaseContext
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<Domain.Entities.TaskList> TaskLists => _database.GetCollection<Domain.Entities.TaskList>("TaskLists");
    }
}