namespace TaskList.Application.Commands;

public class DeleteTaskListCommand
{
    public string TaskListId { get; set; }
    public string CurrentUserId { get; set; }
}