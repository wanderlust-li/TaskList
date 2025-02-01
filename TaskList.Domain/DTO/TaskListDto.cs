namespace TaskList.Domain.DTO;

public class TaskListDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public List<string> SharedWith { get; set; }
}