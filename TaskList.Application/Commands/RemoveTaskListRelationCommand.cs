namespace TaskList.Application.Commands;

public class RemoveTaskListRelationCommand
{
    public string TaskListId { get; set; }
    public string SharedUserId { get; set; }
    public string CurrentUserId { get; set; }
}