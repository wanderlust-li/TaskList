namespace TaskList.Application.Commands;

public class UpdateTaskListCommand
{
    public string TaskListId { get; set; }
    public string NewName { get; set; }
    public string CurrentUserId { get; set; }
}