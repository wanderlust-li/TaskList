namespace TaskList.Application.Queries;

public class GetTaskListsQuery
{
    public string CurrentUserId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}