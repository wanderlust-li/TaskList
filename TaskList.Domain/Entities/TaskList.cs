using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskList.Domain.Entities;

public class TaskList
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
        
    [Required]
    [StringLength(255, MinimumLength = 1)]
    [BsonElement("name")]
    public string Name { get; set; }
        
    [Required]
    [BsonElement("ownerId")]
    public string OwnerId { get; set; }
        
    [BsonElement("createdDateTime")]
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        
    [BsonElement("sharedWith")]
    public List<string> SharedWith { get; set; } = new List<string>();
}