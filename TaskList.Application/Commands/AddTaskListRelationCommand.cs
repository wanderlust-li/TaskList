namespace TaskList.Application.Commands;

public class AddTaskListRelationCommand
{
    public string TaskListId { get; set; }
    public string SharedUserId { get; set; }
    public string CurrentUserId { get; set; }
}