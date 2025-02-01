namespace TaskList.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name = null, object key = null) : base($"{name} ({key}) was not found")
    {
            
    }
}