using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskList.Domain.DTO;

namespace TaskList.Domain.Entities;

public class TaskList
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("ownerId")]
    public string OwnerId { get; set; }
        
    [BsonElement("createdDateTime")]
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        
    [BsonElement("sharedWith")]
    public List<string> SharedWith { get; set; } = new List<string>();
    
    public TaskListDto ToDto()
    {
        return new TaskListDto
        {
            Id = this.Id,
            Name = this.Name,
            OwnerId = this.OwnerId,
            CreatedDateTime = this.CreatedDateTime,
            SharedWith = new List<string>(this.SharedWith)
        };
    }
}