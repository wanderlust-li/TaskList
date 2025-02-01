namespace TaskList.Application.Exceptions;

public class TaskListException : Exception
{
    public TaskListException(string message) : base(message)
    {
    }
}