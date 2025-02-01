namespace TaskList.Application.Queries;

public class GetTaskListRelationsQuery
{
    public string TaskListId { get; set; }
    public string CurrentUserId { get; set; }
}